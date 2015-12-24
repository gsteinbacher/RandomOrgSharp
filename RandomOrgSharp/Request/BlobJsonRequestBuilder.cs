using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public class BlobJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Create(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var blobParameters = parameters as BlobParameters;
            if (blobParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "BlobParameters"));

            var jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, blobParameters.NumberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_SIZE_PARAMETER_NAME, blobParameters.Size),
                new JProperty(RandomOrgConstants.JSON_FORMAT_PARAMETER_NAME, blobParameters.Format.ToString().ToLowerInvariant())
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
