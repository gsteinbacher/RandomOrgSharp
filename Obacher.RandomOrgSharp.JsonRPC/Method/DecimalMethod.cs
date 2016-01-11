using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.RandomOrgSharp.JsonRPC.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Method
{
    public class DecimalMethod : IMethod
    {
        private readonly IRequestBuilder _requestBuilder;
        private readonly IResponseParser _responseParser;

        public DataResponseInfo<decimal> ResponseInfo { get; private set; }

        public DecimalMethod(IRequestBuilder requestBuilder = null, IResponseParser responseParser = null)
        {
            _requestBuilder = requestBuilder ?? new JsonRequestBuilder(new DecimalJsonRequestBuilder());
            _responseParser = responseParser ?? new GenericResponseParser<decimal>();
        }

        public IRequestBuilder CreateRequestBuilder()
        {
            return _requestBuilder;

        }

        public void ParseResponse(string response)
        {
            ResponseInfo = _responseParser.Parse(response) as DataResponseInfo<decimal>;
        }

        public IResponseInfo GetResponseInfo()
        {
            return ResponseInfo;
        }

    }
}