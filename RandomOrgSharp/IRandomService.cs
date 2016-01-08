using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp.Core
{
    public interface IRandomService
    {
        JObject SendRequest(JObject jsonRequest);

        Task<JObject> SendRequestAsync(JObject jsonRequest);
    }
}
