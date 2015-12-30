using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.RandomOrgSharp.Error;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.Method
{
    /// <summary>
    /// Class which handles all the method calls to random.org that return the randome data.  Basically all methods except getUsage and verifySignature.
    /// </summary>
    /// <typeparam name="T">Type of data returned from <see cref="IRandomService"/></typeparam>
    public class DataMethodManager<T> : IDataMethodManager<T>
    {
        private readonly IRandomService _service;
        private readonly IJsonRequestBuilder _requestBuilder;
        private readonly IResponseHandlerFactory _responseHandlerFactory;
        private IAdvisoryDelayManager _advisoryDelayManager;

        public DataMethodManager(IRandomService service = null, IJsonRequestBuilder requestBuilder = null, IResponseHandlerFactory responseHandlerFactory = null, IAdvisoryDelayManager advisoryDelayManager = null)
        {
            _service = service ?? new RandomOrgApiService();
            _responseHandlerFactory = responseHandlerFactory ?? GetDefaultImplementations();
            _advisoryDelayManager = advisoryDelayManager ?? new AdvisoryDelayManager();
            _requestBuilder = requestBuilder ?? new JsonRequestBuilder();
        }

        public DataResponse<T> Generate(IParameters parameters)
        {
            JObject jsonRequest = _requestBuilder.Create(parameters);

            _advisoryDelayManager.Delay();
            JObject jsonResponse = _service.SendRequest(jsonRequest);

            _responseHandlerFactory.Execute(jsonResponse, parameters);

            ApiResponseHandler apiHandler = _responseHandlerFactory.GetHandler(typeof(ApiResponseHandler)) as ApiResponseHandler;

            return apiHandler?.Response as DataResponse<T>;
        }

        /// <summary>
        /// Call method to generate random values in an asynchronous manner
        /// </summary>
        /// <param name="parameters">Parameters for the specific method being called</param>
        /// <returns></returns>
        public async Task<DataResponse<T>> GenerateAsync(IParameters parameters)
        {
            JObject jsonRequest = _requestBuilder.Create(parameters);

            _advisoryDelayManager.Delay();
            JObject jsonResponse = await _service.SendRequestAsync(jsonRequest);

            _responseHandlerFactory.Execute(jsonResponse, parameters);

            ApiResponseHandler apiHandler = _responseHandlerFactory.GetHandler(typeof(ApiResponseHandler)) as ApiResponseHandler;

            return apiHandler?.Response as DataResponse<T>;
        }

        private IResponseHandlerFactory GetDefaultImplementations()
        {
            if (_advisoryDelayManager == null)
                _advisoryDelayManager = new AdvisoryDelayManager(new DateTimeWrap());

            var responseParserFactory =
                new JsonResponseParserFactory(
                    new DefaultMethodParser(),
                    new DataResponseParser<T>(),
                    new UuidResponseParser()
                );

            var factory = new ResponseHandlerFactory(new ErrorHandlerThrowException(),
                        new ApiResponseHandler(responseParserFactory),
                        new AdvisoryDelayResponseHandler(_advisoryDelayManager),
                        new VerifyIdResponseHandler());
            return factory;
        }

    }
}
