using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core
{
    public interface IMethodCallBroker
    {
        bool Generate(IParameters parameters);
        Task<bool> GenerateAsync(IParameters parameters);
    }
}
