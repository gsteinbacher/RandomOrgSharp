using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.JsonRPC.Request
{
    public interface IJsonRequestBuilder
    {
        JObject Build(IParameters parameters);
        bool CanHandle(IParameters parameters);
    }
}