using System;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.Method
{
    /// <summary>
    /// Retrieve a lst of random UUID values
    /// </summary>
    public class UuidMethod
    {
        private bool _verifyOriginater;
        private readonly IDataMethodManager<Guid> _dataMethodManager;

        /// <summary>
        /// Create an instance of <see cref="UuidMethod"/>.  
        /// </summary>
        /// <param name="dataMethodManager">dataMethodManager class to use to retrieve string information.  Default is <see cref="DataMethodManager{T}"/></param>
        public UuidMethod(IDataMethodManager<Guid> dataMethodManager = null)
        {
            _dataMethodManager = dataMethodManager ?? new DataMethodManager<Guid>();
        }

        /// <summary>
        /// Verify the originator of the response.
        /// </summary>
        /// <example>
        /// new BlobManager().WithVerification().GenerateBlobs(...);
        /// </example>
        public UuidMethod WithVerification()
        {
            _verifyOriginater = true;
            return this;
        }

        /// <summary>
        /// Generates version 4 true random Universally Unique IDentifiers (UUIDs) in accordance with section 4.4 of RFC 4122
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random UUID values you need. Must be between 1 and 1000.</param>
        /// <returns>All information returned from random service, include the list of UUID values</returns>
        public DataResponse<Guid> GenerateUuids(int numberOfItemsToReturn)
        {
            var parameters = UuidParameters.Create(numberOfItemsToReturn, _verifyOriginater);
            _verifyOriginater = false;

            var response = _dataMethodManager.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Generates version 4 true random Universally Unique IDentifiers (UUIDs) in accordance with section 4.4 of RFC 4122
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random UUID values you need. Must be between 1 and 1000.</param>
        /// <returns>All information returned from random service, include the list of UUID values</returns>
        public async Task<DataResponse<Guid>> GenerateUuidsAsync(int numberOfItemsToReturn)
        {
            var parameters = UuidParameters.Create(numberOfItemsToReturn, _verifyOriginater);
            _verifyOriginater = false;

            var response = await _dataMethodManager.GenerateAsync(parameters);
            return response;
        }
    }
}


