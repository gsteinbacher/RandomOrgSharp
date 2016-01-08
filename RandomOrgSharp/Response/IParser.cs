using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IParser
    {
        IResponseInfo Parse(JObject json);
        bool CanHandle(IParameters parameters);
    }
}
