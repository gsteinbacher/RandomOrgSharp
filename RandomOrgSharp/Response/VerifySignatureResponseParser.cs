using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public class VerifySignatureResponseParser : IParser
    {
        public IResponseInfo Parse(JObject json)
        {
            var authenticity = false;

            var result = json.GetValue(RandomOrgConstants.JSON_RESULT_PARAMETER_NAME) as JObject;
            if (result != null)
                authenticity = JsonHelper.JsonToBoolean(result.GetValue(RandomOrgConstants.JSON_AUTHENTICITY_PARAMETER_NAME));

            var id = JsonHelper.JsonToInt(json.GetValue("id"));

            var response = new VerifySignatureResponseInfo(authenticity, id);
            return response;
        }

        public bool CanHandle(IParameters parameters)
        {
            return parameters.MethodType == MethodType.VerifySignature;
        }
    }
}