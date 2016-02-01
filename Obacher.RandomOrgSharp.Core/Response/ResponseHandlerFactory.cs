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
        /// Handle the response handlers
        /// </summary>
        /// <param name="parameters">Parameters passed into <see cref="IRandomService"/></param>
        /// <param name="response">Response returnred from <see cref="IRandomService"/></param>
        /// <returns>True is the process should continue to the next <see cref="IResponseHandler"/> in the list, false to stop processing response handlers</returns>
        public bool Execute(IParameters parameters, string response)
        {
            if (_responseHandlers != null)
            {
                foreach (IResponseHandler handlers in _responseHandlers)
                {
                    if (!handlers.Handle(parameters, response))
                        return false;
                }
            }

            return true;
        }

        public IResponseHandler GetHandler(Type responseHandler)
        {
            return _responseHandlers.FirstOrDefault(handler => handler.GetType() == responseHandler);
        }
    }
}