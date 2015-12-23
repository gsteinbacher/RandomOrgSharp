using System;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.Framework.Common.SystemWrapper.Interface;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Properties;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp
{
    /// <summary>
    /// Manage the method calls to random.org.
    /// <remarks>
    /// This method maintains state for some information returned from the call to random.org.
    /// It is expected to only be instantiated once during the lifetime of the application.
    /// </remarks>
    /// </summary>
    public class MethodCallManager : IMethodCallManager, IDisposable
    {
        private readonly IDateTime _dateTimeWrap;
        private long _advisoryDelay;
        private string _apiKey;
        private int _code;

        private bool _isDisposed;
        private IDateTime _lastResponse;
        private string _message;

        /// <summary>
        /// </summary>
        /// <summary>
        /// Instantiates an instance of <see cref="MethodCallManager" />  with <see cref="DateTimeWrap" /> as the class used to
        /// handle the instance of <see cref="DateTime" />.
        /// </summary>
        public MethodCallManager() : this(new DateTimeWrap()) { }

        /// <summary>
        /// Instantiates an instance of <see cref="Obacher.RandomOrgSharp.MethodCallManager" />
        /// </summary>
        /// <param name="dateTimeWrap">Instance of <see cref="IDateTime" /> to handle <see cref="DateTime" /> processing</param>
        public MethodCallManager(IDateTime dateTimeWrap)
        {
            _dateTimeWrap = dateTimeWrap;
            _advisoryDelay = Settings.Default.LastResponse;
        }

        /// <summary>
        /// Is it possible that a call to random.org will be successful
        /// <remarks>
        /// This class does not guarantee that a call to random.org will be successful.  There are a couple conditions that
        /// we know will cause a call to random.org to fail.
        /// This method tests those to condition to be more friendly to random.org by not making the call when it knows a
        /// failure will occur.
        /// The conditions are:
        ///     An error code 400 (The API key you specified does not exist) occurred on the previous call and the API Key has
        ///     not changed.
        ///     random.org only allows so many bits and requests to be sent per day.  If a 402 or 403 are received then this
        ///     method will return <value>false</value> until the date rolls over to the next day (using UTC)
        /// </remarks>
        /// </summary>
        public void CanSendRequest()
        {
            // If an error was returned in a previous call that indicated the API Key was incorrect then check to see if API Key has been changed.
            if (_code == 400)
            {
                var newApiKey = SettingsManager.Instance.GetConfigurationValue<string>(RandomOrgConstants.APIKEY_KEY);
                if (newApiKey == _apiKey)
                    throw new RandomOrgException(_code, _message);
            }

            if (_code == 402 || _code == 403)
            {
                // If the next date has rolled over to a new date then we get a whole new set of requests and bits available
                if (_lastResponse.Equals(_lastResponse.Date, _dateTimeWrap.UtcNow.Date))
                    throw new RandomOrgException(_code, _message);

                _code = 0;
            }
        }

        /// <summary>
        /// Wait until the advisory timeframe has elapsed before continuing
        /// </summary>
        public void Delay()
        {
            if (_advisoryDelay > 0)
            {
                var waitingTime = _dateTimeWrap.UtcNow.Ticks - _advisoryDelay;
                if (waitingTime > 0)
                    Thread.Sleep(TimeSpan.FromTicks(waitingTime * 10000));
            }
        }

        /// <summary>
        /// Store the advisory delay so it can be used in the <c>Delay</c> method
        /// </summary>
        /// <param name="advisoryDelay"></param>
        public void SetAdvisoryDelay(int advisoryDelay)
        {
            _advisoryDelay = _dateTimeWrap.UtcNow.Ticks + advisoryDelay;
        }

        /// <summary>
        /// Throw an exception if an error is returned from the call to www.random.org
        /// </summary>
        /// <param name="jsonResponse">JSON response returned from call to www.random.org</param>
        public void ThrowExceptionOnError(JObject jsonResponse)
        {
            _code = 0;
            var result = jsonResponse.GetValue(RandomOrgConstants.JSON_ERROR_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                _code = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_CODE_PARAMETER_NAME));
                var data = result.GetValue(RandomOrgConstants.JSON_DATA_PARAMETER_NAME);

                if (!data.HasValues)
                    _message = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + _code);
                else
                {
                    var unformattedMessage = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + _code);
                    if (!string.IsNullOrWhiteSpace(unformattedMessage))
                        _message = string.Format(unformattedMessage, data.Values<object>().ToArray());
                }

                if (string.IsNullOrWhiteSpace(_message))
                    _message = JsonHelper.JsonToString(result.GetValue(RandomOrgConstants.JSON_MESSAGE_PARAMETER_NAME));

                // If the API key is invalid then store it for subsequent calls.  If it is the same in subsequent calls then
                // this same exception can be thrown without sending a request to random.org.
                if (_code == 400)
                    _apiKey = SettingsManager.Instance.GetConfigurationValue<string>(RandomOrgConstants.APIKEY_KEY);

                if (_code == 402 || _code == 403)
                    _lastResponse = _dateTimeWrap.UtcNow;

                throw new RandomOrgException(_code, _message);
            }
        }

        /// <summary>
        /// Verify the response is the one expected to be returned from the request sent.
        /// </summary>
        /// <param name="requestParameters">Request information</param>
        /// <param name="response">Response information</param>
        public void VerifyResponse(IParameters requestParameters, IResponse response)
        {
            if (requestParameters.Id != response.Id)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.IDS_NOT_MATCHED));
        }

        #region Dispose

        /// <summary>
        /// Dispose instance
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Save the advisory delay to ensure it is used the next time this class is instantiated
                    // This is a precaution in case the developer using this class is instantiating it inside a loop
                    Settings.Default.LastResponse = _advisoryDelay;
                    Settings.Default.Save();
                }

                _isDisposed = true;
            }
        }

        #endregion
    }
}