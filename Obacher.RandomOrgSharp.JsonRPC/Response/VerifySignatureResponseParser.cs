using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{
    public class VerifySignatureResponseParser : IResponseParser
    {
        public IResponseInfo Parse(string response)
        {
            var authenticity = false;

            JObject jsonResponse = JObject.Parse(response);
            var result = jsonResponse.GetValue(JsonRpcConstants.RESULT_PARAMETER_NAME) as JObject;
            if (result != null)
                authenticity = JsonHelper.JsonToBoolean(result.GetValue(JsonRpcConstants.AUTHENTICITY_PARAMETER_NAME));

            var id = JsonHelper.JsonToInt(jsonResponse.GetValue("id"));

            var responseInfo = new VerifySignatureResponseInfo(authenticity, id);
            return responseInfo;
        }

        public bool CanHandle(IParameters parameters)
        {
            return parameters.VerifyOriginator;
        }
    }
}