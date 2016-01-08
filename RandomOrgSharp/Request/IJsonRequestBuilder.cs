using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public interface IJsonRequestBuilder
    {
        JObject Create(IParameters parameters);
        bool CanHandle(IParameters parameters);
    }
}