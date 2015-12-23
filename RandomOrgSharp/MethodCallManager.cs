using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.Framework.Common.SystemWrapper.Interface;
using Obacher.RandomOrgSharp.Properties;

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
        private int _code;
        private string _message;
        private string _apiKey;
        private IDateTime _lastResponse;
        private long _advisoryDelay;

        private bool _isDisposed;

        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// Instantiates an instance of <see cref="MethodCallManager" />  with <see cref="DateTimeWrap"/> as the class used to handle the instance of <see cref="DateTime"/>.
        /// </summary>
        public MethodCallManager() : this(new DateTimeWrap()) { }

        /// <summary>
        /// Instantiates an instance of <see cref="Obacher.RandomOrgSharp.MethodCallManager" /> 
        /// </summary>
        /// <param name="dateTimeWrap">Instance of <see cref="IDateTime"/> to handle <see cref="DateTime"/> processing</param>
        public MethodCallManager(IDateTime dateTimeWrap)
        {
            _dateTimeWrap = dateTimeWrap;
            _advisoryDelay = Settings.Default.LastResponse;
        }

        /// <summary>
        /// Is it possible that a call to random.org will be successful
        /// <remarks>
        /// This class does not guarantee that a call to random.org will be successful.  There are a couple conditions that we know will cause a call to random.org to fail.
        /// This method tests those to condition to be more friendly to random.org by not making the call when it knows a failure will occur.
        /// The condittions are:
        ///    An error code 400 (The API key you specified does not exist) occurred on the previous call and the API Key has not changed.
        ///    random.org only allows so many bits and requests to be sent per day.  If a 402 or 403 are received then this method will return <value>false</value> until the date rolls
        ///    over to the next day (using UTC)
        /// </remarks>
        /// </summary>
        public void CanSendRequest()
        {
            // If an error was returned in a previous call that indicated the API Key was incorrect then check to see if API Key has been changed.
            if (_code == 400)
            {
                string newApiKey = SettingsManager.Instance.GetConfigurationValue<string>(RandomOrgConstants.APIKEY_KEY);
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

        public void Delay()
        {
            if (_advisoryDelay > 0)
            {
                long waitingTime = _dateTimeWrap.UtcNow.Ticks - _advisoryDelay;
                if (waitingTime > 0)
                    Thread.Sleep(TimeSpan.FromTicks(waitingTime * 10000));
            }
        }

        public void SetAdvisoryDelay(int advisoryDelay)
        {
            _advisoryDelay = _dateTimeWrap.UtcNow.Ticks + advisoryDelay;
        }

        /// <summary>
        /// Check for an error being returned and throw a <see cref="RandomOrgException"></see> when an error is returned />
        /// </summary>
        /// <param name="jsonResponse">JSON response returned from call to www.random.org</param>
        public void ThrowExceptionOnError(JObject jsonResponse)
        {
            _code = 0;
            var result = jsonResponse.GetValue(RandomOrgConstants.JSON_ERROR_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                _code = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_CODE_PARAMETER_NAME));
                JToken data = result.GetValue(RandomOrgConstants.JSON_DATA_PARAMETER_NAME);

                if (!data.HasValues)
                    _message = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + _code);
                else
                {
                    string unformattedMessage = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + _code);
                    if (!string.IsNullOrWhiteSpace(unformattedMessage))
                        _message = string.Format(unformattedMessage, data.Values<object>().ToArray());
                }

                if (string.IsNullOrWhiteSpace(_message))
                    _message = JsonHelper.JsonToString(result.GetValue(RandomOrgConstants.JSON_MESSAGE_PARAMETER_NAME));

                // If the API key is invalid then store it for subsequent calls
                if (_code == 400)
                    _apiKey = SettingsManager.Instance.GetConfigurationValue<string>(RandomOrgConstants.APIKEY_KEY);

                if (_code == 402 || _code == 403)
                    _lastResponse = _dateTimeWrap.UtcNow;

                throw new RandomOrgException(_code, _message);
            }
        }

        public void VerifyResponse(IRequestParameters requestParameters, IResponse response)
        {
            if (requestParameters.Id != response.Id)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.IDS_NOT_MATCHED));
        }

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
    }
}
