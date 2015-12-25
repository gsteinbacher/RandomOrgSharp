using System.Collections.Generic;
using System.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class JsonResponseParserFactory : IJsonResponseParserFactory
    {
        private readonly IParser _defaultParser;
        private readonly IList<IParser> _parsers;

        /// <summary>
        /// Instantiate the factory to handle retrieval of the parser needed for the json returned from the current request to random.org.  
        /// </summary>
        /// <param name="defaultParser">The default parser if a specific parser is not found</param>
        /// <param name="parsers">List of parser to use</param>
        public JsonResponseParserFactory(IParser defaultParser, params IParser[] parsers)
        {
            _defaultParser = defaultParser;
            _parsers = parsers.ToList();
        }

        public IParser GetParser(IParameters parameters)
        {
            return _parsers.FirstOrDefault(m => m.CanHandle(parameters)) ?? _defaultParser;
        }
    }
}