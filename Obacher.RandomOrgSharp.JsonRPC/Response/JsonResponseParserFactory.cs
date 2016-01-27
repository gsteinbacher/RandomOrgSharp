using System.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{
    public class JsonResponseParserFactory : IResponseHandler
    {
        private readonly IResponseParser[] _parsers;

        public IResponseInfo ResponseInfo { get; private set; }

        /// <summary>
        /// Instantiate the factory to handle retrieval of the parser needed for the json returned from the current request to random.org.  
        /// </summary>
        /// <param name="parsers">List of parser to use</param>
        public JsonResponseParserFactory(params IResponseParser[] parsers)
        {
            _parsers = parsers;
        }

        public bool Handle(IParameters parameters, string response)
        {
            IResponseParser parser = _parsers.FirstOrDefault(m => m.CanHandle(parameters));
            if (parser == null)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.ERROR_CODE_100));

            ResponseInfo = parser.Parse(response);

            return true;
        }

        public bool CanHandle(IParameters parameters)
        {
            // Every response can be parsed in some manner
            return true;
        }
    }
}