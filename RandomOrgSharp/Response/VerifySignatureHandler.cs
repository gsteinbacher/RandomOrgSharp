using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
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
        /// <param name="parameters"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public bool Process(IParameters parameters, JObject json)
        {
            var result = json.GetValue(RandomOrgConstants.JSON_RESULT_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                var random = result.GetValue(RandomOrgConstants.JSON_RANDOM_PARAMETER_NAME) as JObject;
                var id = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_ID_PARAMETER_NAME));

                if (random != null)
                {
                    var signature = JsonHelper.JsonToString(result.GetValue(RandomOrgConstants.JSON_SIGNATURE_PARAMETER_NAME));

                    var jsonParameters = new JObject(
                        new JProperty(RandomOrgConstants.JSON_RANDOM_PARAMETER_NAME, random),
                        new JProperty(RandomOrgConstants.JSON_SIGNATURE_PARAMETER_NAME, signature));

                    var jsonRequest = new JObject(
                        new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                        new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, RandomOrgConstants.VERIFY_SIGNATURE_METHOD),
                        new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME, jsonParameters),
                        new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, id)
                        );

                    JObject verifyResponse = _service.SendRequest(jsonRequest);

                    var verifyResult = verifyResponse.GetValue(RandomOrgConstants.JSON_RESULT_PARAMETER_NAME) as JObject;
                    var authenticity = verifyResult != null && JsonHelper.JsonToBoolean(verifyResult.GetValue(RandomOrgConstants.JSON_AUTHENTICITY_PARAMETER_NAME));

                    if (!authenticity)
                        throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.JSON_NOT_VERIFIED));
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