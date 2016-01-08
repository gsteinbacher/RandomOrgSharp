using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public class VerifySignatureRequestBuilder : IJsonRequestBuilder
    {
        public JObject Create(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var verifySignatureParameters = parameters as VerifySignatureParameters;
            if (verifySignatureParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "UuidParameters"));

            var jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_RANDOM_PARAMETER_NAME, verifySignatureParameters.Result),
                new JProperty(RandomOrgConstants.JSON_SIGNATURE_PARAMETER_NAME, verifySignatureParameters.Signature));

            var jsonRequest = new JObject(
                new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, RandomOrgConstants.VERIFY_SIGNATURE_METHOD),
                new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME, jsonParameters),
                new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, parameters.Id)
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
