using System;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public interface IRequestHandlerFactory
    {
        bool Execute(IParameters parameters);
        IRequestHandler GetHandler(Type handlerType);

    }
}
