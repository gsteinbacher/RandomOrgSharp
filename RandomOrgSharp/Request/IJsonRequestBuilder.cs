using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public interface IJsonRequestBuilder
    {
        JObject Create(IParameters parameters);
        bool CanHandle(IParameters parameters);
    }
}