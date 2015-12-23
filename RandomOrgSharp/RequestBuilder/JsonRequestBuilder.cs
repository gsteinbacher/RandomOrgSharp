using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.RequestBuilder
{
    public class JsonRequestBuilder : IRequestBuilder
    {
        private readonly CommonParameters _parameters;

        public JsonRequestBuilder(CommonParameters parameters)
        {
            _parameters = parameters;
        }

        public JObject Create(IParameterBuilder parameterBuilder)
        {
            JObject parameters = parameterBuilder.Create();
            parameters.Add(RandomOrgConstants.APIKEY_KEY, _parameters.ApiKey);

            JObject jsonRequest = new JObject(
                new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, _parameters.Method),
                new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME, parameters),
                new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, _parameters.Id)
            );

            return jsonRequest;
        }
    }
}