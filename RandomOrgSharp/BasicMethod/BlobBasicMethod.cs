using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    /// <summary>
    /// Retrieve a lst of random blob values
    /// </summary>
    public class BlobBasicMethod
    {
        private readonly IBasicMethod<string> _basicMethod;

        /// <summary>
        /// Create an instance of <see cref="BlobBasicMethod"/>.  
        /// </summary>
        /// <param name="basicMethod">BasicMethod class to use to retrieve blob information.  Default is <see cref="BasicMethod{T}"/></param>
        public BlobBasicMethod(IBasicMethod<string> basicMethod = null)
        {
            if (basicMethod == null)
                _basicMethod = new BasicMethod<string>(new RandomOrgApiService(), new MethodCallManager(), new JsonRequestBuilder(), new BasicMethodResponseParser<string>());

            _basicMethod = basicMethod;
        }

        /// <summary>
        /// Retrieve a list of random blob values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random blob values you need. Must be between 1 and 100.</param>
        /// <param name="size">The size of each blob, measured in bits. Must be between 1 and 1048576 and must be divisible by 8.</param>
        /// <param name="format">Specifies the format in which the blobs will be returned, default value is Base64</param>
        /// <returns>All information returned from random service, include the list of blob values</returns>
        public IBasicMethodResponse<string> GenerateBlobs(int numberOfItemsToReturn, int size, BlobFormat format)
        {
            var parameters = BlobParameters.Set(numberOfItemsToReturn, size, format);

            var response = _basicMethod.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Retrieve a list of random blob values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random blob values you need. Must be between 1 and 100.</param>
        /// <param name="size">The size of each blob, measured in bits. Must be between 1 and 1048576 and must be divisible by 8.</param>
        /// <param name="format">Specifies the format in which the blobs will be returned, default value is Base64</param>
        /// <returns>All information returned from random service, include the list of blob values</returns>
        public async Task<IBasicMethodResponse<string>> GenerateBlobsAsync(int numberOfItemsToReturn, int size, BlobFormat format)
        {
            var parameters = BlobParameters.Set(numberOfItemsToReturn, size, format);

            var response = await _basicMethod.GenerateAsync(parameters);
            return response;
        }
    }
}
