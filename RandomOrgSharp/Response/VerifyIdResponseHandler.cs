using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class VerifyIdResponseHandler : IResponseHandler
    {
        public bool Process(JObject json, IParameters parameters)
        {
            var id = JsonHelper.JsonToInt(json.GetValue("id"));

            if (id != parameters.Id)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.IDS_NOT_MATCHED));

            // If we get down to here then the Ids match
            return true;
        }
    }
}