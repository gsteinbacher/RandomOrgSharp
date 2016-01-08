using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.Abstration
{
    /// <summary>
    /// Retrieve a lst of random blob values
    /// </summary>
    public class BlobMethod
    {
        private readonly IMethodCallBroker _methodCallBroker;
        private bool _verifyOriginater;

        /// <summary>
        /// Create an instance of <see cref="BlobMethod"/>.  
        /// </summary>
        /// <param name="methodCallBroker">methodCallBroker class to use to retrieve blob information.  Default is <see cref="MethodCallBroker{T}"/></param>
        public BlobMethod(IMethodCallBroker methodCallBroker = null)
        {
            _methodCallBroker = methodCallBroker ?? new MethodCallBroker();
        }

        /// <summary>
        /// Verify the originator of the responseInfo.
        /// </summary>
        /// <example>
        /// new BlobMethod().WithVerification().GenerateBlobs(...);
        /// </example>
        public BlobMethod WithVerification()
        {
            _verifyOriginater = true;
            return this;
        }

        /// <summary>
        /// Retrieve a list of random blob values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random blob values you need. Must be between 1 and 100.</param>
        /// <param name="size">The size of each blob, measured in bits. Must be between 1 and 1048576 and must be divisible by 8.</param>
        /// <param name="format">Specifies the format in which the blobs will be returned, default value is Base64</param>
        /// <returns>All information returned from random service, include the list of blob values</returns>
        public DataResponseInfo<string> GenerateBlobs(int numberOfItemsToReturn, int size, BlobFormat format = BlobFormat.Base64)
        {
            var parameters = BlobParameters.Create(numberOfItemsToReturn, size, format, _verifyOriginater);
            _verifyOriginater = false;

            var response = _methodCallBroker.Generate(parameters);
            return response as DataResponseInfo<string>;
        }

        /// <summary>
        /// Retrieve a list of random blob values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random blob values you need. Must be between 1 and 100.</param>
        /// <param name="size">The size of each blob, measured in bits. Must be between 1 and 1048576 and must be divisible by 8.</param>
        /// <param name="format">Specifies the format in which the blobs will be returned, default value is Base64</param>
        /// <returns>All information returned from random service, include the list of blob values</returns>
        public async Task<DataResponseInfo<string>> GenerateBlobsAsync(int numberOfItemsToReturn, int size, BlobFormat format = BlobFormat.Base64)
        {
            var parameters = BlobParameters.Create(numberOfItemsToReturn, size, format, _verifyOriginater);
            _verifyOriginater = false;

            var response = await _methodCallBroker.GenerateAsync(parameters);
            return response as DataResponseInfo<string>;
        }
    }
}
