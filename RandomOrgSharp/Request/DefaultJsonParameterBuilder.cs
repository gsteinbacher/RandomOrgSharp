using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public class DefaultJsonParameterBuilder : IRequestBuilder
    {
        public JObject Create(IParameters parameters)
        {
            // There are no additional parameters for the Usage command
            return null;
        }

        /// <summary>
        /// Identify this class as one that handles Blob parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <returns>True if this class handles the specified parameters</returns>
        public bool CanHandle(IParameters parameters)
        {
            return false;
        }

    }
}
