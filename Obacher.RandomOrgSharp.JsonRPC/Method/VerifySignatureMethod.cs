﻿using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.RandomOrgSharp.JsonRPC.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Method
{
    public class VerifySignatureMethod : IMethod
    {
        private readonly IRequestBuilder _requestBuilder;
        private readonly IResponseParser _responseParser;

        public VerifySignatureResponseInfo ResponseInfo { get; private set; }

        public VerifySignatureMethod(IRequestBuilder requestBuilder = null, IResponseParser responseParser = null)
        {
            _requestBuilder = requestBuilder ?? new JsonRequestBuilder(new UuidJsonRequestBuilder());
            _responseParser = responseParser ?? new UuidResponseParser();
        }

        public IRequestBuilder CreateRequestBuilder()
        {
            return _requestBuilder;

        }

        public void ParseResponse(string response)
        {
            ResponseInfo = _responseParser.Parse(response) as VerifySignatureResponseInfo;
        }

        public IResponseInfo GetResponseInfo()
        {
            return ResponseInfo;
        }
    }
}