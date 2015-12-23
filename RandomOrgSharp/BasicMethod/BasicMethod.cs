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

        public BasicMethod(IRandomOrgService service, IMethodCallManager methodCallManager)
        {
            _service = service;
            _methodCallManager = methodCallManager;
        }

        public IBasicMethodResponse<T> Generate(IRequestBuilder requestBuilder, IParser basicMethodResponseParser, IParameters parameters)
        {
            _methodCallManager.CanSendRequest();

            JObject jsonRequest = requestBuilder.Create(parameters);

            _methodCallManager.Delay();
            JObject jsonResponse = _service.SendRequest(jsonRequest);

            IBasicMethodResponse<T> response = HandleResponse(jsonResponse, basicMethodResponseParser, parameters);

            return response;
        }

        public async Task<IBasicMethodResponse<T>> GenerateAsync(IRequestBuilder requestBuilder, IParser basicMethodResponseParser, IParameters parameters)
        {
            _methodCallManager.CanSendRequest();

            JObject jsonRequest = requestBuilder.Create(parameters);

            _methodCallManager.Delay();
            JObject jsonResponse = await _service.SendRequestAsync(jsonRequest);

            IBasicMethodResponse<T> response = HandleResponse(jsonResponse, basicMethodResponseParser, parameters);

            return response;
        }

        private IBasicMethodResponse<T> HandleResponse(JObject jsonResponse, IParser basicMethodResponseParser, IParameters parameters)
        {
            _methodCallManager.ThrowExceptionOnError(jsonResponse);

            IResponse response = basicMethodResponseParser.Parse(jsonResponse);
            _methodCallManager.SetAdvisoryDelay(response.AdvisoryDelay);

            _methodCallManager.VerifyResponse(parameters, response);

            return response as IBasicMethodResponse<T>;
        }

    }
}
