using System.Threading.Tasks;

namespace Obacher.RandomOrgSharp.Core
{
    public interface IRandomService
    {
        string SendRequest(string request);

        Task<string> SendRequestAsync(string request);
    }
}
