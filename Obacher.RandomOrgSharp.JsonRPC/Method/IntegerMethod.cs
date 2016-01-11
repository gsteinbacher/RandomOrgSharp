using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.RandomOrgSharp.JsonRPC.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Method
{
    public class IntegerMethod : IMethod
    {
        private readonly IRequestBuilder _requestBuilder;
        private readonly IResponseParser _responseParser;

        public DataResponseInfo<int> ResponseInfo { get; private set; }

        public IntegerMethod(IRequestBuilder requestBuilder = null, IResponseParser responseParser = null)
        {
            _requestBuilder = requestBuilder ?? new JsonRequestBuilder(new GuassianJsonRequestBuilder());
            _responseParser = responseParser ?? new GenericResponseParser<int>();
        }

        public IRequestBuilder CreateRequestBuilder()
        {
            return _requestBuilder;

        }

        public void ParseResponse(string response)
        {
            ResponseInfo = _responseParser.Parse(response) as DataResponseInfo<int>;
        }

        public IResponseInfo GetResponseInfo()
        {
            return ResponseInfo;
        }

    }
}