using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.Method
{
    public interface IDataMethodManager<T>
    {
        DataResponse<T> Generate(IParameters parameters);
        Task<DataResponse<T>> GenerateAsync(IParameters parameters);
    }
}
