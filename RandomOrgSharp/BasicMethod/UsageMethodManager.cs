using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class UsageMethodManager
    {
        private readonly IRandomOrgService _service;
        private readonly IMethodCallManager _methodCallManager;
        private readonly IJsonRequestBuilder _requestBuilder;
        private readonly IJsonResponseParserFactory _responseParserFactory;

        public UsageMethodManager(IRandomOrgService service = null, IMethodCallManager methodCallManager = null, IJsonRequestBuilder requestBuilder = null, IJsonResponseParserFactory responseParserFactory = null)
        {
            _service = service ?? new RandomOrgApiService();
            _methodCallManager = methodCallManager ?? new MethodCallManager();
            _requestBuilder = requestBuilder ?? new JsonRequestBuilder();
            _responseParserFactory = responseParserFactory ??
                new JsonResponseParserFactory(
                    new DefaultMethodParser(),
                    new UsageMethodResponseParser()
                );
        }

        public UsageMethodResponse Generate(IParameters parameters)
        {
            JObject jsonRequest = _requestBuilder.Create(parameters);

            _methodCallManager.Delay();
            JObject jsonResponse = _service.SendRequest(jsonRequest);

            UsageMethodResponse response = HandleResponse(jsonResponse, parameters);

            return response;
        }

        public async Task<UsageMethodResponse> GenerateAsync(IParameters parameters)
        {
            JObject jsonRequest = _requestBuilder.Create(parameters);

            _methodCallManager.Delay();
            JObject jsonResponse = await _service.SendRequestAsync(jsonRequest);

            UsageMethodResponse response = HandleResponse(jsonResponse, parameters);

            return response;
        }

        private UsageMethodResponse HandleResponse(JObject jsonResponse, IParameters parameters)
        {
            _methodCallManager.ThrowExceptionOnError(jsonResponse);

            IParser parser = _responseParserFactory.GetParser(parameters);
            UsageMethodResponse response = parser.Parse(jsonResponse) as UsageMethodResponse;
            if (response != null)
                _methodCallManager.VerifyResponse(parameters, response);

            return response;
        }

    }
}
