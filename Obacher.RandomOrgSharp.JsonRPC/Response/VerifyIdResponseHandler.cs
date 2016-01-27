using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{
    /// <summary>
    /// Ensure the ID value from the request object matches the ID value from the response object
    /// </summary>
    public class VerifyIdResponseHandler : IResponseHandler
    {
        /// <summary>
        /// Verify the ID's from the request and response objects match
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool Handle(IParameters parameters, string response)
        {
            JObject jsonResponse = JObject.Parse(response);
            int id = JsonHelper.JsonToInt(jsonResponse.GetValue(JsonRpcConstants.ID_PARAMETER_NAME, 0));
            if (id != parameters.Id)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.IDS_NOT_MATCHED));

            // If we get down to here then the Ids match
            return true;
        }

        /// <summary>
        /// Every method call can handle the verification of the Id number
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool CanHandle(IParameters parameters)
        {
            return true;
        }
    }
}