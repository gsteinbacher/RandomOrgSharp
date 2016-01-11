using System;
using System.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public class RequestCommandFactory : IRequestCommandFactory
    {
        private readonly IRequestCommand[] _requestHandlers;

        public RequestCommandFactory(params IRequestCommand[] requestHandlers)
        {
            _requestHandlers = requestHandlers;
        }

        public bool Execute(IParameters parameters)
        {
            foreach (IRequestCommand handlers in _requestHandlers)
            {
                if (handlers.CanHandle(parameters))
                {
                    if (!handlers.Process(parameters))
                        return false;
                }
            }
            return true;
        }
    }
}
