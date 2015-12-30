using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class ApiResponseHandler : IResponseHandler
    {
        private readonly IJsonResponseParserFactory _responseParserFactory;

        public IResponse Response { get; private set; }

        public ApiResponseHandler(IJsonResponseParserFactory responseParserFactory)
        {
            _responseParserFactory = responseParserFactory;
        }

        public bool Process(JObject json, IParameters parameters)
        {
            IParser parser = _responseParserFactory.GetParser(parameters);
            Response = parser.Parse(json);

            return Response != null;
        }
    }
}