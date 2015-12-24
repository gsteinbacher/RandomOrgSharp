using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public interface IParser
    {
        IResponse Parse(JObject json);
        bool CanHandle(IParameters parameters);
    }
}
