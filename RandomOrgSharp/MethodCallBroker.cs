using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
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
        private readonly IRequestHandlerFactory _requestHandlerFactory;
        private readonly IRandomService _service;
        private readonly IJsonRequestBuilder _requestBuilder;
        private readonly IResponseHandlerFactory _responseHandlerFactory;
        private readonly IJsonResponseParserFactory _responseParserFactory;

        public MethodCallBroker(IRandomService service = null, IJsonRequestBuilder requestBuilder = null, IRequestHandlerFactory requestHandlerFactory = null, IJsonResponseParserFactory responseParserFactory = null, IResponseHandlerFactory responseHandlerFactory = null)
        {
            _requestHandlerFactory = requestHandlerFactory ?? new RequestHandlerFactory(new AdvisoryDelayHandler());
            _service = service ?? new RandomOrgApiService();
            _responseParserFactory = responseParserFactory ?? GetDefaultParserFactory();
            _responseHandlerFactory = responseHandlerFactory ?? GetDefaultResponseHandlerFactory();
            _requestBuilder = requestBuilder ?? new JsonRequestBuilder();
        }

        public IResponseInfo Generate(IParameters parameters)
        {
            JObject jsonRequest = _requestBuilder.Create(parameters);

            _requestHandlerFactory.Execute(parameters);

            JObject jsonResponse = _service.SendRequest(jsonRequest);

            IParser parser = _responseParserFactory.GetParser(parameters);
            IResponseInfo responseInfo = parser.Parse(jsonResponse);

            _responseHandlerFactory.Execute(responseInfo, parameters);

            return responseInfo;
        }

        /// <summary>
        /// Call method to generate random values in an asynchronous manner
        /// </summary>
        /// <param name="parameters">Parameters for the specific method being called</param>
        /// <returns></returns>
        public async Task<IResponseInfo> GenerateAsync(IParameters parameters)
        {
            JObject jsonRequest = _requestBuilder.Create(parameters);

            _requestHandlerFactory.Execute(parameters);
            JObject jsonResponse = await _service.SendRequestAsync(jsonRequest);

            IParser parser = _responseParserFactory.GetParser(parameters);
            IResponseInfo responseInfo = parser.Parse(jsonResponse);

            _responseHandlerFactory.Execute(responseInfo, parameters);

            return responseInfo;
        }

        private IResponseHandlerFactory GetDefaultResponseHandlerFactory()
        {
            var factory = new ResponseHandlerFactory(
                    new ErrorHandlerThrowException(),
                    new AdvisoryDelayHandler(),
                    new VerifyIdResponseHandler(),
                    new VerifySignatureHandler());
            return factory;
        }

        private IJsonResponseParserFactory GetDefaultParserFactory()
        {
            return new JsonResponseParserFactory(
                new DefaultMethodParser(),
                new DataResponseParser<T>,
                new UuidResponseParser(),
                new UsageResponseParser()
    );
        }
    }
}
