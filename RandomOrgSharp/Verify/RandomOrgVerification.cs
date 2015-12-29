using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Verify
{
    public class RandomOrgVerification
    {
        private readonly IRandomOrgService _service;
        public RandomOrgVerification(IRandomOrgService service = null)
        {
            _service = service ?? new RandomOrgApiService();
        }

        public void Verify(JObject json)
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
        }
    }
}