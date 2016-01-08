using System;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IResponseHandlerFactory
    {
        bool Execute(IResponseInfo responseInfo, IParameters parameters);
        IResponseHandler GetHandler(Type handlerType);
    }
}