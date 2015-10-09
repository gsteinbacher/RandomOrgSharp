using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.RequestParameters
{
    public abstract class CommonRequestParameters : IRequestParameters
    {
        private static string _apiKey;

        protected CommonRequestParameters()
        {
            //_apiKey = SettingsManager.Instance.GetConfigurationValue<string>(RandomOrgConstants.APIKEY_KEY);
            _apiKey = RandomOrgConstants.APIKEY_VALUE;
            if (_apiKey == null)
                throw new RandomOrgRunTimeException(9999, Strings.ResourceManager.GetString(ResourceConstants.APIKEY_REQUIRED));
        }

        protected JObject CreateJsonRequestInternal(string method, JObject parameters)
        {
            parameters.Add(RandomOrgConstants.APIKEY_KEY, _apiKey);

            JObject jsonParams = new JObject(
                new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                new JProperty(RandomOrgConstants.METHOD_PARAMETER_NAME, method),
                new JProperty(RandomOrgConstants.PARAMETERS_PARAMETER_NAME, parameters),
                new JProperty(RandomOrgConstants.ID_PARAMETER_NAME, RandomNumberGenerator.Instance.Next()));

            return jsonParams;
        }

        public abstract JObject CreateJsonRequest();
    }
}
