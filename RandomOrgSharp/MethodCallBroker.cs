using System.Threading.Tasks;

using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.Core
{
    /// <summary>
    /// Class which handles all the method calls to random.org that return the randome data.  Basically all methods except getUsage and verifySignature.
    /// </summary>
    public class MethodCallBroker : IMethodCallBroker
    {
        private readonly IRandomService _service;
        private readonly IMethod _method;
        private readonly IErrorHandler _errorHandler;
        private readonly IRequestCommandFactory _requestCommandFactory;
        private readonly IResponseHandlerFactory _responseHandlerFactory;


        public MethodCallBroker(IMethod method, IErrorHandler errorHandler, IRandomService service = null, IRequestCommandFactory requestCommandFactory = null, IResponseHandlerFactory responseHandlerFactory = null)
        {
            _method = method;
            _errorHandler = errorHandler;

            _service = service ?? new RandomOrgApiService();
            _requestCommandFactory = requestCommandFactory;
            _responseHandlerFactory = responseHandlerFactory;
        }

        public void Generate(IParameters parameters)
        {
            IRequestBuilder requestBuilder = _method.CreateRequestBuilder();
            string request = requestBuilder.Build(parameters);

            _requestCommandFactory?.Execute(parameters);

            string response = _service.SendRequest(request);

                _method.ParseResponse(response);
                _responseHandlerFactory?.Execute(parameters, response);
        }

        /// <summary>
        /// Call method to generate random values in an asynchronous manner
        /// </summary>
        /// <param name="parameters">Parameters for the specific method being called</param>
        /// <returns></returns>
        public async void GenerateAsync(IParameters parameters)
        {
            IRequestBuilder requestBuilder = _method.CreateRequestBuilder();
            string request = requestBuilder.Build(parameters);

            _requestCommandFactory?.Execute(parameters);

            string response = await _service.SendRequestAsync(request);

            _errorHandler.Process(response);
            if (!_errorHandler.HasError())
            {
                _method.ParseResponse(response);
                _responseHandlerFactory?.Execute(parameters, _method.GetResponseInfo());
            }
        }

        //private IResponseHandlerFactory GetDefaultResponseHandlerFactory()
        //{
        //    var factory = new ResponseHandlerFactory(
        //            new AdvisoryDelayHandler(),
        //            new VerifyIdResponseHandler());
        //    return factory;
        //}
    }
}
