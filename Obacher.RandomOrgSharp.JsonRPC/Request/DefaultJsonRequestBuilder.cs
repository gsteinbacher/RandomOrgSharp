using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.JsonRPC.Request
{
    public class DefaultJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Build(IParameters parameters)
        {
            // Default request builder contains no specific parameters
            return null;
        }
    }
}
