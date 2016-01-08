using System.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Properties;

namespace Obacher.RandomOrgSharp.Core.Response
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

        /// <summary>
        /// Return the appropriate parser 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns><see cref="IParser"/></returns>
        public IParser GetParser(IParameters parameters)
        {
            IParser parser = _parsers.FirstOrDefault(m => m.CanHandle(parameters));
            if (parser == null)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(Strings.ERROR_CODE_100));

            return parser;
        }
    }
}