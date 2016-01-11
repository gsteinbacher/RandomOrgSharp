using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.Core
{
    public interface IMethodCallBroker
    {
        void Generate(IParameters parameters);
        void GenerateAsync(IParameters parameters);
    }
}
