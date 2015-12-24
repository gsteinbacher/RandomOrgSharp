using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class BasicMethod<T> : IBasicMethod<T>
    {
        private readonly IRandomOrgService _service;
        private readonly IMethodCallManager _methodCallManager;
        private readonly IJsonRequestBuilder _requestBuilder;
        private readonly IParser _responseParser;

        public BasicMethod(IRandomOrgService service, IMethodCallManager methodCallManager, IJsonRequestBuilder requestBuilder, IParser basicMethodResponseParser)
        {
            _service = service;
            _methodCallManager = methodCallManager;
            _requestBuilder = requestBuilder;
            _responseParser = basicMethodResponseParser;
        }

        public IBasicMethodResponse<T> Generate(IParameters parameters)
        {
            _methodCallManager.CanSendRequest();

            JObject jsonRequest = _requestBuilder.Create(parameters);

            _methodCallManager.Delay();
            JObject jsonResponse = _service.SendRequest(jsonRequest);

            IBasicMethodResponse<T> response = HandleResponse(jsonResponse, parameters);

            return response;
        }

        public async Task<IBasicMethodResponse<T>> GenerateAsync(IParameters parameters)
        {
            _methodCallManager.CanSendRequest();

            JObject jsonRequest = _requestBuilder.Create(parameters);

            _methodCallManager.Delay();
            JObject jsonResponse = await _service.SendRequestAsync(jsonRequest);

            IBasicMethodResponse<T> response = HandleResponse(jsonResponse, parameters);

            return response;
        }

        private IBasicMethodResponse<T> HandleResponse(JObject jsonResponse, IParameters parameters)
        {
            _methodCallManager.ThrowExceptionOnError(jsonResponse);

            IBasicMethodResponse<T> response = _responseParser.Parse(jsonResponse) as IBasicMethodResponse<T>;
            if (response != null)
            {
                _methodCallManager.SetAdvisoryDelay(response.AdvisoryDelay);
                _methodCallManager.VerifyResponse(parameters, response);
            }

            return response;
        }

    }
}
