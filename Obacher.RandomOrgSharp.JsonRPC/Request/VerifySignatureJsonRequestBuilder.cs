using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.JsonRPC.Request
{
    public class VerifySignatureJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Build(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var verifySignatureParameters = parameters as VerifySignatureParameters;
            if (verifySignatureParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "UuidParameters"));

            var jsonParameters = new JObject(
                new JProperty(JsonRpcConstants.RANDOM_PARAMETER_NAME, verifySignatureParameters.Result),
                new JProperty(JsonRpcConstants.SIGNATURE_PARAMETER_NAME, verifySignatureParameters.Signature));

            var jsonRequest = new JObject(
                new JProperty(JsonRpcConstants.RPC_PARAMETER_NAME, JsonRpcConstants.RPC_VALUE),
                new JProperty(JsonRpcConstants.METHOD_PARAMETER_NAME, RandomOrgConstants.VERIFY_SIGNATURE_METHOD),
                new JProperty(JsonRpcConstants.PARAMETERS_PARAMETER_NAME, jsonParameters),
                new JProperty(JsonRpcConstants.ID_PARAMETER_NAME, parameters.Id)
                );

            return jsonParameters;
        }

        /// <summary>
        /// Identify this class as one that handles UUID parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <returns>True if this class handles the specified parameters</returns>
        public bool CanHandle(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return parameters.MethodType == MethodType.VerifySignature;
        }
    }
}
