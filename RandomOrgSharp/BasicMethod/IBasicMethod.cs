using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp.RequestParameters;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public interface IBasicMethod
    {
        IResponse Execute(IRequestParameters requestParameters);
        Task<IResponse> ExecuteAsync(IRequestParameters requestParameters);
        bool CanExecute(MethodType methodType);
    }
}
