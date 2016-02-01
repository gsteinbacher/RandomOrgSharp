using System.Threading.Tasks;

using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.Core.Service;

namespace Obacher.RandomOrgSharp.Core
{
    /// <summary>
    /// Class which handles all the method calls to random.org that return the randome data.  Basically all methods except getUsage and verifySignature.
    /// </summary>
    public class MethodCallBroker : IMethodCallBroker
    {
        private readonly IRandomService _service;
        private readonly IRequestBuilder _requestBuilder;
        private readonly IBeforeRequestCommandFactory _beforeRequestCommandFactory;
        private readonly IResponseHandlerFactory _responseHandlerFactory;


        public MethodCallBroker(IRequestBuilder requestBuilder, IRandomService service = null, IBeforeRequestCommandFactory beforeRequestCommandFactory = null, IResponseHandlerFactory responseHandlerFactory = null)
        {
            _requestBuilder = requestBuilder;
            _service = service ?? new RandomOrgApiService();
            _beforeRequestCommandFactory = beforeRequestCommandFactory;
            _responseHandlerFactory = responseHandlerFactory;
        }

        public bool Generate(IParameters parameters)
        {
            string request = _requestBuilder == null ? string.Empty : _requestBuilder.Build(parameters);

            _beforeRequestCommandFactory?.Execute(parameters);

            string response = _service.SendRequest(request);

            bool result = true;
            if (_responseHandlerFactory != null)
                result = _responseHandlerFactory.Execute(parameters, response);

            return result;
        }

        /// <summary>
        /// Call method to generate random values in an asynchronous manner
        /// </summary>
        /// <param name="parameters">Parameters for the specific method being called</param>
        /// <returns>Response object that is returned from service</returns>
        public async Task<bool> GenerateAsync(IParameters parameters)
        {
            string request = _requestBuilder == null ? string.Empty : _requestBuilder.Build(parameters);

            _beforeRequestCommandFactory?.Execute(parameters);

            string response = await _service.SendRequestAsync(request);

            bool result = true;
            if (_responseHandlerFactory != null)
                result = _responseHandlerFactory.Execute(parameters, response);

            return result;
        }
    }
}
