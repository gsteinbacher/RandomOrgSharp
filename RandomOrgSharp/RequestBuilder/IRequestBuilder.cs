using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp.RequestBuilder
{
    public interface IRequestBuilder
    {
        JObject Create(IParameterBuilder parameterBuilder);

    }
}