using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.Response
{
    /// <summary>
    /// Class which parses the responses from Blob, Decimal, Guassian, Integer and String method calls.  
    /// All these methods return the same information except for the type so I use the same method for all of them.
    /// </summary>
    public class DefaultMethodParser : IParser
    {
        /// <summary>
        /// Parse the JSON response
        /// </summary>
        /// <param name="json">JSON response</param>
        /// <returns>Class which contains the information from the JSON response</returns>
        public IResponse Parse(JObject json)
        {
            // All methods should have a specific parser so throw an exception indicating the method is not implemented
            throw new RandomOrgRunTimeException(ResourceHelper.GetString(Strings.ERROR_CODE_100));
        }

        /// <summary>
        /// Identifies the types of responses that are handled by this class
        /// </summary>
        /// <param name="parameters">Parameters which are utilized by class</param>
        /// <returns>True if this class handles the method call</returns>
        public bool CanHandle(IParameters parameters)
        {
            // Default parser is never "handled"
            return false;
        }
    }
}