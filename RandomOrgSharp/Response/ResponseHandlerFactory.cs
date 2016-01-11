using System;
using System.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public class ResponseHandlerFactory : IResponseHandlerFactory
    {
        private readonly IResponseHandler[] _responseHandlers;

        public ResponseHandlerFactory(params IResponseHandler[] responseHandlers)
        {
            _responseHandlers = responseHandlers;
        }

        /// <summary>
        /// Execute the response handlers
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="responseInfo"></param>
        /// <returns></returns>
        public bool Execute(IParameters parameters, IResponseInfo responseInfo)
        {
            foreach (IResponseHandler handlers in _responseHandlers)
            {
                if (!handlers.Process(parameters, responseInfo))
                    return false;
            }
            return true;
        }

        public bool Execute(IResponseInfo responseInfo, IParameters parameters)
        {
            throw new NotImplementedException();
        }

        public IResponseHandler GetHandler(Type handlerType)
        {
            return _responseHandlers.FirstOrDefault(handler => handler.GetType() == handlerType);
        }
    }
}