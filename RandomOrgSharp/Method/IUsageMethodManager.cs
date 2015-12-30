using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.Method
{
    public interface IUsageMethodManager
    {
        UsageResponse Get(IParameters parameters);
        Task<UsageResponse> GetAsync(IParameters parameters);
    }
}