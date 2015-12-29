using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public interface IBasicMethodManager<T>
    {
        IBasicMethodResponse<T> Generate(IParameters parameters);
        Task<IBasicMethodResponse<T>> GenerateAsync(IParameters parameters);
    }
}
