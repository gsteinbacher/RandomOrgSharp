using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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

        public IEnumerable<T> Generate(IRequestParameters requestParameters)
        {
            _methodCallManager.CanSendRequest();

            JObject jsonRequest = requestParameters.CreateJsonRequest();

            _methodCallManager.Delay();
            JObject jsonReponse = _service.SendRequest(jsonRequest);

            BasicMethodResponse response = HandleResponse(requestParameters, jsonReponse);

            return response.Data.Values<T>();
        }


        public async Task<IEnumerable<T>> GenerateAsync(IRequestParameters requestParameters)
        {
            _methodCallManager.CanSendRequest();

            JObject jsonRequest = requestParameters.CreateJsonRequest();

            _methodCallManager.Delay();
            JObject jsonReponse = await _service.SendRequestAsync(jsonRequest);

            var response = HandleResponse(requestParameters, jsonReponse);

            return response.Data.Values<T>();
        }


        private BasicMethodResponse HandleResponse(IRequestParameters requestParameters, JObject jsonReponse)
        {
            _methodCallManager.ThrowExceptionOnError(jsonReponse);

            BasicMethodResponse response = BasicMethodResponse.Parse(jsonReponse);
            _methodCallManager.SetAdvisoryDelay(response.AdvisoryDelay);

            _methodCallManager.VerifyResponse(requestParameters, response);

            return response;
        }

    }
}
