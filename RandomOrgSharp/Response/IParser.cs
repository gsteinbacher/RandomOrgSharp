using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp.Response
{
    public interface IParser
    {
        IResponse Parse(JObject json);
    }
}
