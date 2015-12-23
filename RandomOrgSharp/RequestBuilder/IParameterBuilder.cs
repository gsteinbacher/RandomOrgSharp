using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp.RequestBuilder
{
    public interface IParameterBuilder
    {
        JObject Create();
    }
}