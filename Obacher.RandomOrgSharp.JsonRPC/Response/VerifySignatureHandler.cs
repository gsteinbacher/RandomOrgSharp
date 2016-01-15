using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{
    /// <summary>
    /// Verify the response came from random.org and was not tampered with.
    /// </summary>
    public class VerifySignatureHandler : IResponseHandler
    {
        private readonly IRandomService _service;
        public VerifySignatureHandler(IRandomService service = null)
        {
            _service = service ?? new RandomOrgApiService();
        }

        /// <summary>
        /// Call random.org to verify the response came from random.org and was not tampered before received
        /// </summary>
        /// <param name="parameters">Parameters passed into the request object</param>
        /// <param name="response">Response returned from <see cref="IRandomService"/></param>
        /// <returns>True if the signature is verified</returns>
        public bool Execute(IParameters parameters, string response)
        {
            JObject jsonbResponse = JObject.Parse(response);

            var result = jsonbResponse.GetValue(JsonRpcConstants.RESULT_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                var random = result.GetValue(JsonRpcConstants.RANDOM_PARAMETER_NAME) as JObject;
                var id = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.ID_PARAMETER_NAME));

                if (random != null)
                {
                    var signature = JsonHelper.JsonToString(result.GetValue(JsonRpcConstants.SIGNATURE_PARAMETER_NAME));

                    var jsonParameters = new JObject(
                        new JProperty(JsonRpcConstants.RANDOM_PARAMETER_NAME, random),
                        new JProperty(JsonRpcConstants.SIGNATURE_PARAMETER_NAME, signature));

                    var jsonRequest = new JObject(
                        new JProperty(JsonRpcConstants.RPC_PARAMETER_NAME, JsonRpcConstants.RPC_VALUE),
                        new JProperty(JsonRpcConstants.METHOD_PARAMETER_NAME, JsonRpcConstants.VERIFY_SIGNATURE_METHOD),
                        new JProperty(JsonRpcConstants.PARAMETERS_PARAMETER_NAME, jsonParameters),
                        new JProperty(JsonRpcConstants.ID_PARAMETER_NAME, id)
                        );

                    string verifyResponse = _service.SendRequest(jsonRequest.ToString());

                    JObject jsonVerifyResponse = JObject.Parse(verifyResponse);
                    var verifyResult = jsonVerifyResponse.GetValue(JsonRpcConstants.RESULT_PARAMETER_NAME) as JObject;
                    var authenticity = verifyResult != null && JsonHelper.JsonToBoolean(verifyResult.GetValue(JsonRpcConstants.AUTHENTICITY_PARAMETER_NAME));

                    if (!authenticity)
                        throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.NOT_VERIFIED));
                }
            }

            return true;
        }

        /// <summary>
        /// The signature needs to be verified when the VerifyOriginator is true
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>True if the VerifyOriginator is true</returns>
        public bool CanHandle(IParameters parameters)
        {
            return parameters.VerifyOriginator;
        }
    }
}