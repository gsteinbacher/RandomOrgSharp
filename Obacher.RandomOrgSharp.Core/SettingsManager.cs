using Obacher.Framework.Common.SystemWrapper;
using Obacher.Framework.Common.SystemWrapper.Interface;

namespace Obacher.RandomOrgSharp.Core
{
    public sealed class SettingsManager : ISettingsManager
    {
        private const string DefaultUrl = "https://api.random.org/json-rpc/1/invoke";
        private const string UrlKey = "Url";

        private const string HttpRequestTimeoutKey = "HttpRequestTimeout";
        private const string HttpReadwriteTimeoutKey = "HttpReadWriteTimeout";
        private const int DefaultRequestTimeout = 180000;
        private const int DefaultReadwriteTimeout = 180000;


        private readonly IConfigurationManager _configurationManager;

        public SettingsManager(IConfigurationManager configurationManager = null)
        {
            _configurationManager = configurationManager ?? new ConfigurationManagerWrap();
        }

        public string GetApiKey()
        {
            string apiKey = _configurationManager.GetAppSettingValue<string>(RandomOrgConstants.APIKEY_KEY);
            if (apiKey == null)
                throw new RandomOrgRuntimeException(ResourceHelper.GetString(StringsConstants.APIKEY_REQUIRED));

            return apiKey;
        }

        public string GetUrl()
        {
            return _configurationManager.GetAppSettingValue(UrlKey, DefaultUrl);
        }

        public int GetHttpRequestTimeout()
        {
            return _configurationManager.GetAppSettingValue(HttpRequestTimeoutKey, DefaultRequestTimeout);
        }

        public int GetHttpReadWriteTimeout()
        {
            return _configurationManager.GetAppSettingValue(HttpReadwriteTimeoutKey, DefaultReadwriteTimeout);
        }
    }
}
