using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public interface IBasicMethod<T>
    {
        IEnumerable<T> Generate(IRequestParameters requestParameters);
        Task<IEnumerable<T>> GenerateAsync(IRequestParameters requestParameters);
    }
}
