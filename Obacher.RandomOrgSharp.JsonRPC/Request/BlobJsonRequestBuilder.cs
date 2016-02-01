using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.JsonRPC.Request
{
    public class BlobJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Build(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var blobParameters = parameters as BlobParameters;
            if (blobParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "BlobParameters"));

            var jsonParameters = new JObject(
                new JProperty(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, blobParameters.NumberOfItemsToReturn),
                new JProperty(JsonRpcConstants.SIZE_PARAMETER_NAME, blobParameters.Size),
                new JProperty(JsonRpcConstants.FORMAT_PARAMETER_NAME, blobParameters.Format.ToString().ToLowerInvariant())
                );

            return jsonParameters;
        }


        /// <summary>
        /// Identify this class as one that handles Blob parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <returns>True if this class handles the specified parameters</returns>
        public bool CanHandle(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return parameters.MethodType == MethodType.Blob;
        }
    }
}
