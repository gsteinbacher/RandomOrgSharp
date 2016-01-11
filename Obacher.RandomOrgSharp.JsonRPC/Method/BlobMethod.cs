using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.RandomOrgSharp.JsonRPC.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Method
{
    public class BlobMethod : IMethod
    {
        private readonly IRequestBuilder _requestBuilder;
        private readonly IResponseParser _responseParser;

        public DataResponseInfo<string> ResponseInfo { get; private set; }

        public BlobMethod(IRequestBuilder requestBuilder = null, IResponseParser responseParser = null)
        {
            _requestBuilder = requestBuilder ?? new JsonRequestBuilder(new BlobJsonRequestBuilder());
            _responseParser = responseParser ?? new GenericResponseParser<string>();
        }

        public IRequestBuilder CreateRequestBuilder()
        {
            return _requestBuilder;

        }

        public void ParseResponse(string response)
        {
            ResponseInfo = _responseParser.Parse(response) as DataResponseInfo<string>;
        }

        public IResponseInfo GetResponseInfo()
        {
            return ResponseInfo;
        }

    }
}