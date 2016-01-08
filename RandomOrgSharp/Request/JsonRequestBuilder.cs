using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public class JsonRequestBuilder : IJsonRequestBuilder
    {
        private readonly IJsonRequestBuilderFactory _factory;

        public JsonRequestBuilder(IJsonRequestBuilderFactory factory = null)
        {
            // Setup default JsonParameterBuilderFactory
            if (factory == null)
                factory = JsonRequestBuilderFactory.GetDefaultBuilders();

            _factory = factory;
        }


        public JObject Create(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var commonParameters = parameters as CommonParameters;
            if (commonParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "CommonParameters"));

            var parameterBuilder = _factory.GetBuilder(parameters);
            var jsonParameters = parameterBuilder.Create(parameters) ?? new JObject();
            jsonParameters.Add(RandomOrgConstants.APIKEY_KEY, commonParameters.ApiKey);

            var jsonRequest = new JObject(
                new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, commonParameters.GetMethodName()),
                new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME, jsonParameters),
                new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, commonParameters.Id)
                );

            return jsonRequest;
        }

        public bool CanHandle(IParameters parameters)
        {
            return false;
        }
    }
}