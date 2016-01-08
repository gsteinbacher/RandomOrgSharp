using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.Core
{
    public interface IMethodCallBroker
    {
        IResponseInfo Generate(IParameters parameters);
        Task<IResponseInfo> GenerateAsync(IParameters parameters);
    }
}
