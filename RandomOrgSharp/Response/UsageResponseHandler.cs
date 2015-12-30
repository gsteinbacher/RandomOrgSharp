using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class UsageResponseHandler<T> : IResponseHandler
    {
        private readonly IJsonResponseParserFactory _responseParserFactory;

        public UsageResponse Response { get; private set; }

        public UsageResponseHandler(IJsonResponseParserFactory responseParserFactory)
        {
            _responseParserFactory = responseParserFactory;
        }

        public bool Process(JObject json, IParameters parameters)
        {
            IParser parser = _responseParserFactory.GetParser(parameters);
            Response = parser.Parse(json) as UsageResponse;

            return Response != null;
        }

        public bool IsHandlerOfType(string handlerType)
        {
            return string.Equals(handlerType, RandomOrgConstants.HANDLER_TYPE_API, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}