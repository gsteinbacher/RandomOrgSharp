using System;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IResponseHandlerFactory
    {
        bool Execute(IParameters parameters, IResponseInfo responseInfo);
        IResponseHandler GetHandler(Type handlerType);
    }
}