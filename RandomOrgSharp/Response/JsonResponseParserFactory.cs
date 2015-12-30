using System.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class JsonResponseParserFactory : IJsonResponseParserFactory
    {
        private readonly IParser[] _parsers;

        /// <summary>
        /// Instantiate the factory to handle retrieval of the parser needed for the json returned from the current request to random.org.  
        /// </summary>
        /// <param name="parsers">List of parser to use</param>
        public JsonResponseParserFactory(params IParser[] parsers)
        {
            _parsers = parsers;
        }

        public IParser GetParser(IParameters parameters)
        {
            return _parsers.FirstOrDefault(m => m.CanHandle(parameters));
        }
    }
}