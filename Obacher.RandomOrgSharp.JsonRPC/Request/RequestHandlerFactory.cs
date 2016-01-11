using System;
using System.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Request;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public class RequestHandlerFactory : IRequestHandlerFactory
    {
        private readonly IRequestHandler[] _requestHandlers;

        public RequestHandlerFactory(params IRequestHandler[] requestHandlers)
        {
            _requestHandlers = requestHandlers;
        }

        public bool Execute(IParameters parameters)
        {
            foreach (IRequestHandler handlers in _requestHandlers)
            {
                if (handlers.CanHandle(parameters))
                {
                    if (!handlers.Process(parameters))
                        return false;
                }
            }
            return true;
        }

        public IRequestHandler GetHandler(Type handlerType)
        {
            return _requestHandlers.FirstOrDefault(handler => handler.GetType() == handlerType);
        }

        IRequestHandler IRequestHandlerFactory.GetHandler(Type handlerType)
        {
            throw new NotImplementedException();
        }
    }
}