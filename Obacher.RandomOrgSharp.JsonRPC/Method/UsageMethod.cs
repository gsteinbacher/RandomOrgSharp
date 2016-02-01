using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.Core.Service;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.RandomOrgSharp.JsonRPC.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Method
{
    /// <summary>
    /// This class is a wrapper class the <see cref="MethodCallBroker"/>.  It setups a default set of classes to be called when making a call to random.org
    /// </summary>
    /// <remarks>
    /// The following is the sequence of actions that will occur during the call...
    ///     Build the necessary JSON-RPC request string (the package that is used to retrieve a list of blob values)
    ///     Execute the <see cref="AdvisoryDelayHandler"/> to delay the request the time recommended by random.org (based on the value in the previous response)
    ///     Retrieve the random blob values from random.org
    ///     Determine if an error occurred during the call and throw an exception if one did occur
    ///     Store the advisory delay value to ensure the next request to random.org does not occur before the time requested by random.org.  This will help prevent you from
    ///     being banned from making requests to random.org
    ///     Verify the Id returned in the response matches the id passed into the request
    ///     Parse the response into a <see cref="DataResponseInfo{T}"/> so the blob values can be extracted
    /// </remarks>
    public class UsageMethod
    {
        private readonly IRandomService _service;
        private readonly IRequestBuilder _requestBuilder;
        private readonly IResponseHandlerFactory _responseHandlerFactory;
        private readonly JsonResponseParserFactory _responseParser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="randomService"><see cref="IRandomService"/> to use to get random values.  Defaults to <see cref="RandomOrgApiService"/></param>
        public UsageMethod(IRandomService randomService = null)
        {
            _service = randomService ?? new RandomOrgApiService();

            _requestBuilder = new JsonRequestBuilder();

            // We need to keep this separate so we can retrieve the list of values that are returned from to the caller
            _responseParser = new JsonResponseParserFactory(new UsageResponseParser());

            _responseHandlerFactory = new ResponseHandlerFactory(
                new ErrorHandlerThrowException(new ErrorParser()),
                _responseParser
            );
        }

        /// <summary>
        /// Retrieve the usage information
        /// </summary>
        /// <returns>Usage information</returns>
        public UsageResponseInfo GetUsage()
        {
            IParameters requestParameters = UsageParameters.Create();
            IMethodCallBroker broker = new MethodCallBroker(_requestBuilder, _service, null, _responseHandlerFactory);
            broker.Generate(requestParameters);

            return _responseParser.ResponseInfo as UsageResponseInfo;
        }

        /// <summary>
        /// Retrieve the usage information in an asynchronous manners
        /// </summary>
        /// <returns>Usage information</returns>
        public async Task<UsageResponseInfo> GetUsageAsync()
        {
            IParameters requestParameters = UsageParameters.Create();
            MethodCallBroker broker = new MethodCallBroker(_requestBuilder, _service, null, _responseHandlerFactory);
            await broker.GenerateAsync(requestParameters);

            return _responseParser.ResponseInfo as UsageResponseInfo;
        }
    }
}