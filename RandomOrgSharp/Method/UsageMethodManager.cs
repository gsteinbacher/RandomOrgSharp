using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.RandomOrgSharp.Error;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.Method
{
    public class UsageMethodManager : IUsageMethodManager
    {
        private readonly IRandomService _service;
        private readonly IJsonRequestBuilder _requestBuilder;
        private readonly IResponseHandlerFactory _responseHandlerFactory;


        public UsageMethodManager(IRandomService service = null, IJsonRequestBuilder requestBuilder = null, IResponseHandlerFactory responseHandlerFactory = null)
        {
            _responseHandlerFactory = responseHandlerFactory ?? GetDefaultImplementations();
            _service = service ?? new RandomOrgApiService();
            _requestBuilder = requestBuilder ?? new JsonRequestBuilder();
        }

        private IResponseHandlerFactory GetDefaultImplementations()
        {
            var responseParserFactory =
                new JsonResponseParserFactory(
                    new UsageResponseParser()
                );

            var factory = new ResponseHandlerFactory(new ErrorHandlerThrowException(),
                        new ApiResponseHandler(responseParserFactory),
                        new VerifyIdResponseHandler());
            return factory;
        }

        public UsageResponse Get(IParameters parameters)
        {
            JObject jsonRequest = _requestBuilder.Create(parameters);

            JObject jsonResponse = _service.SendRequest(jsonRequest);
            _responseHandlerFactory.Execute(jsonResponse, parameters);

            ApiResponseHandler apiHandler = _responseHandlerFactory.GetHandler(typeof(ApiResponseHandler)) as ApiResponseHandler;

            return apiHandler?.Response as UsageResponse;
        }

        public async Task<UsageResponse> GetAsync(IParameters parameters)
        {
            JObject jsonRequest = _requestBuilder.Create(parameters);

            JObject jsonResponse = await _service.SendRequestAsync(jsonRequest);
            _responseHandlerFactory.Execute(jsonResponse, parameters);

            ApiResponseHandler apiHandler = _responseHandlerFactory.GetHandler(typeof(ApiResponseHandler)) as ApiResponseHandler;

            return apiHandler?.Response as UsageResponse;
        }
    }
}
