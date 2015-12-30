using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp
{
    public interface IRandomService
    {
        JObject SendRequest(JObject jsonRequest);

        Task<JObject> SendRequestAsync(JObject jsonRequest);
    }
}
