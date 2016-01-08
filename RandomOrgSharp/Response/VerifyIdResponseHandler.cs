using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
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
        /// <param name="json"></param>
        /// <returns></returns>
        public bool Process(IParameters parameters, JObject json)
        {
            var id = JsonHelper.JsonToInt(json.GetValue("id"));

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