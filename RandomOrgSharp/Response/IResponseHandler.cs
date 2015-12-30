using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public interface IResponseHandler
    {
        /// <summary>
        /// Process the response 
        /// </summary>
        /// <param name="json">Response object in JSON format</param>
        /// <param name="parameters">Parameters passed into the request object</param>
        /// <returns>True if the process can continue to process subsequent handlers</returns>
        bool Process(JObject json, IParameters parameters);
    }
}