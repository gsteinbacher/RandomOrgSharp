using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
    {
        public class UsageMethod
        {
            private readonly IRandomOrgService _service;
            private readonly IMethodCallManager _methodCallManager;

            public UsageMethod(IRandomOrgService service, IMethodCallManager methodCallManager)
            {
                _service = service;
                _methodCallManager = methodCallManager;
            }

            public IUsageMethodResponse Generate(IRequestBuilder requestBuilder, IParser basicMethodResponseParser, IParameters parameters)
            {
                _methodCallManager.CanSendRequest();

                JObject jsonRequest = requestBuilder.Create();

                _methodCallManager.Delay();
                JObject jsonResponse = _service.SendRequest(jsonRequest);

            IUsageMethodResponse response = HandleResponse(jsonResponse, basicMethodResponseParser, parameters);

                return response;
            }

            public async Task<IUsageMethodResponse> GenerateAsync(IRequestBuilder requestBuilder, IParser basicMethodResponseParser, IParameters parameters)
            {
                _methodCallManager.CanSendRequest();

                JObject jsonRequest = requestBuilder.Create();

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

}
}