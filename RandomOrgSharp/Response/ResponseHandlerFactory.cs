using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class ResponseHandlerFactory : IResponseHandlerFactory
    {
        private readonly IResponseHandler[] _responseHandlers;

        public ResponseHandlerFactory(params IResponseHandler[] responseHandlers)
        {
            _responseHandlers = responseHandlers;
        }

        public bool Execute(JObject json, IParameters parameters)
        {
            foreach (IResponseHandler handlers in _responseHandlers)
            {
                if (!handlers.Process(json, parameters))
                    return false;
            }
            return true;
        }

        public IResponseHandler GetHandler(Type handlerType)
        {
            return _responseHandlers.FirstOrDefault(handler => handler.GetType() == handlerType);
        }
    }
}