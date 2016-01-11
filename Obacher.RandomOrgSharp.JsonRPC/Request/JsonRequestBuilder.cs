using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.JsonRPC.Request
{
    public class JsonRequestBuilder : IRequestBuilder
    {
        private readonly IJsonRequestBuilder _builder;

        public JsonRequestBuilder(IJsonRequestBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Create a string that will be passed into the <see cref="IRandomService"/>.  The string contains all the parameters in the format expected by the <see cref="IRandomService"/>
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>String representing the request object to be passed to the <see cref="IRandomService"/></returns>
        public string Build(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var commonParameters = parameters as CommonParameters;
            if (commonParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "CommonParameters"));

            var jsonParameters = _builder.Build(parameters) ?? new JObject();
            jsonParameters.Add(JsonRpcConstants.APIKEY_KEY, commonParameters.ApiKey);

            var jsonRequest = new JObject(
                new JProperty(JsonRpcConstants.RPC_PARAMETER_NAME, JsonRpcConstants.RPC_VALUE),
                new JProperty(JsonRpcConstants.METHOD_PARAMETER_NAME, commonParameters.GetMethodName()),
                new JProperty(JsonRpcConstants.PARAMETERS_PARAMETER_NAME, jsonParameters),
                new JProperty(JsonRpcConstants.ID_PARAMETER_NAME, commonParameters.Id)
                );

            return jsonRequest.ToString();
        }
    }
}