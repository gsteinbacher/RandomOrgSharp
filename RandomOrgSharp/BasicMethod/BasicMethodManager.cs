using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class BasicMethodManager<T> : IBasicMethodManager<T>
    {
        private readonly IRandomOrgService _service;
        private readonly IMethodCallManager _methodCallManager;
        private readonly IJsonRequestBuilder _requestBuilder;
        private readonly IJsonResponseParserFactory _responseParserFactory;

        public BasicMethodManager(IRandomOrgService service = null, IMethodCallManager methodCallManager = null, IJsonRequestBuilder requestBuilder = null, IJsonResponseParserFactory responseParserFactory = null)
        {
            _service = service ?? new RandomOrgApiService();
            _methodCallManager = methodCallManager ?? new MethodCallManager();
            _requestBuilder = requestBuilder ?? new JsonRequestBuilder();
            _responseParserFactory = responseParserFactory ??
                new JsonResponseParserFactory(
                    new DefaultMethodParser(),
                    new BasicMethodResponseParser<T>(),
                    new UuidResponseParser(),
                    new UsageMethodResponseParser()
                );
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

            if (parameters.VerifyOriginator)
            {
                var result = jsonResponse.GetValue(RandomOrgConstants.JSON_RESULT_PARAMETER_NAME) as JObject;
                var random = result?.GetValue(RandomOrgConstants.JSON_RANDOM_PARAMETER_NAME) as JObject;
                if (random != null)
                {
                    var signature = JsonHelper.JsonToString(result.GetValue(RandomOrgConstants.JSON_SIGNATURE_PARAMETER_NAME));

                    var jsonParameters = new JObject(
                        new JProperty(RandomOrgConstants.JSON_RANDOM_PARAMETER_NAME, random),
                        new JProperty(RandomOrgConstants.JSON_SIGNATURE_PARAMETER_NAME, signature));

                    var jsonRequest = new JObject(
                        new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                        new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, RandomOrgConstants.VERIFY_SIGNATURE_METHOD),
                        new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME, jsonParameters),
                        new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, parameters.Id)
                        );

                    JObject verifyResponse = _service.SendRequest(jsonRequest);

                    var verifyResult = verifyResponse.GetValue(RandomOrgConstants.JSON_RESULT_PARAMETER_NAME) as JObject;
                    var authenticity = verifyResult != null && JsonHelper.JsonToBoolean(verifyResult.GetValue(RandomOrgConstants.JSON_AUTHENTICITY_PARAMETER_NAME));
                    if (!authenticity)
                        throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.JSON_NOT_VERIFIED));
                }
            }

            IParser parser = _responseParserFactory.GetParser(parameters);
            IBasicMethodResponse<T> response = parser.Parse(jsonResponse) as IBasicMethodResponse<T>;
            if (response != null)
            {
                _methodCallManager.SetAdvisoryDelay(response.AdvisoryDelay);
                _methodCallManager.VerifyResponse(parameters, response);
            }

            return response;
        }

    }
}
