using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public interface IBasicMethod<T>
    {
        IBasicMethodResponse<T> Generate(IRequestBuilder requestBuilder, IParser responseParser, IParameters parameters);
        Task<IBasicMethodResponse<T>> GenerateAsync(IRequestBuilder requestBuilder, IParser responseParser, IParameters parameters);
    }
}
