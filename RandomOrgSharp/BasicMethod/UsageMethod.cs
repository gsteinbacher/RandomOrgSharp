using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    /// <summary>
    /// Retrieve a lst of random integer values
    /// </summary>
    public class UsageMethod
    {
        private readonly IRandomOrgService _service;
        private readonly IMethodCallManager _methodCallManager;
        private readonly IJsonRequestBuilder _requestBuilder;
        private readonly IParser _responseParser;

        /// <summary>
        /// Create an instance of <see cref="StringBasicMethod"/>.  
        /// </summary>
        public UsageMethod(IRandomOrgService service, IMethodCallManager methodCallManager, IJsonRequestBuilder requestBuilder, IParser basicMethodResponseParser)
        {
            _service = service;
            _methodCallManager = methodCallManager;
            _requestBuilder = requestBuilder;
            _responseParser = basicMethodResponseParser;
        }

        /// <summary>
        /// Returns information related to the the usage of a given API key.
        /// </summary>
        /// <returns>Information related to usage of a given API key.</returns>
        public IResponse GetUsage()
        {
            _methodCallManager.CanSendRequest();

            // Usage method has no specific parameters
            var parameters = new UsageParameters();
            JObject jsonRequest = _requestBuilder.Create(parameters);

            _methodCallManager.Delay();
            JObject jsonResponse = _service.SendRequest(jsonRequest);

            IResponse response = HandleResponse(jsonResponse, parameters);

            return response;
        }


        /// <summary>
        /// Returns information related to the the usage of a given API key as an asynchronous operation.
        /// </summary>
        /// <returns>Information related to usage of a given API key.</returns>
        public async Task<IResponse> GetUsageAsync()
        {
            _methodCallManager.CanSendRequest();

            // Usage method has no specific parameters
            var parameters = new UsageParameters();
            JObject jsonRequest = _requestBuilder.Create(parameters);

            _methodCallManager.Delay();
            JObject jsonResponse = await _service.SendRequestAsync(jsonRequest);

            IResponse response = HandleResponse(jsonResponse, parameters);

            return response;
        }

        private IResponse HandleResponse(JObject jsonResponse, IParameters parameters)
        {
            _methodCallManager.ThrowExceptionOnError(jsonResponse);

            IResponse response = _responseParser.Parse(jsonResponse);
            if (response != null)
                _methodCallManager.VerifyResponse(parameters, response);

            return response;
        }

    }
}


