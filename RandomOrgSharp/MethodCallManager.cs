﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Properties;
using Obacher.RandomOrgSharp.RequestParameters;

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
        private int _code;
        private string _message;
        private string _apiKey;
        private DateTime _lastResponse;
        private long _advisoryDelay;

        public MethodCallManager()
        {
            _advisoryDelay = Settings.Default.LastResponse;
        }

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
                if (_lastResponse.Date == DateTime.UtcNow.Date)
                    throw new RandomOrgException(_code, _message);

                _code = 0;
            }
        }

        public void Delay()
        {
            if (_advisoryDelay > 0)
            {
                long waitingTime = DateTime.UtcNow.Ticks - _advisoryDelay;
                if (waitingTime > 0)
                    Thread.Sleep(TimeSpan.FromTicks(waitingTime * 10000));
            }
        }

        public void SetAdvisoryDelay(int advisoryDelay)
        {
            _advisoryDelay = DateTime.UtcNow.Ticks + advisoryDelay;
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

                if (data == null)
                    _message = Strings.ResourceManager.GetString(StringsConstants.ERROR_CODE_KEY + _code);
                else
                {
                    IEnumerable<string> dataList = data.Values<string>() ?? Enumerable.Empty<string>();

                    string unformattedMessage = Strings.ResourceManager.GetString(StringsConstants.ERROR_CODE_KEY + _code);
                    if (!string.IsNullOrWhiteSpace(unformattedMessage))
                        _message = string.Format(unformattedMessage, dataList);
                }

                if (string.IsNullOrWhiteSpace(_message))
                    _message = JsonHelper.JsonToString(result.GetValue(RandomOrgConstants.JSON_MESSAGE_PARAMETER_NAME));

                // If the API key is invalid then store it for subsequent calls
                if (_code == 400)
                    _apiKey = SettingsManager.Instance.GetConfigurationValue<string>(RandomOrgConstants.APIKEY_KEY);

                if (_code == 402 || _code == 403)
                    _lastResponse = DateTime.UtcNow;

                throw new RandomOrgException(_code, _message);
            }
        }

        public void VerifyResponse(IRequestParameters requestParameters, BasicMethodResponse response)
        {
            if (requestParameters.Id != response.Id)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.IDS_NOT_MATCHED));
        }

        public void Dispose()
        {
            // Save the advisory delay to ensure it is used the next time this class is instantiated
            // This is a precaution in case the developer using this class is instantiating it inside a loop
            Settings.Default.LastResponse = _advisoryDelay;
            Settings.Default.Save();
        }
    }
}
