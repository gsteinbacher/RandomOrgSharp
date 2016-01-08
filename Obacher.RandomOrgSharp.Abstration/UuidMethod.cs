using System;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.Abstration
{
    /// <summary>
    /// Retrieve a lst of random UUID values
    /// </summary>
    public class UuidMethod
    {
        private bool _verifyOriginater;
        private readonly IMethodCallBroker _methodCallBroker;

        /// <summary>
        /// Create an instance of <see cref="UuidMethod"/>.  
        /// </summary>
        /// <param name="methodCallBroker">methodCallBroker class to use to retrieve string information.  Default is <see cref="MethodCallBroker{T}"/></param>
        public UuidMethod(IMethodCallBroker methodCallBroker = null)
        {
            _methodCallBroker = methodCallBroker ?? new MethodCallBroker<Guid>();
        }

        /// <summary>
        /// Verify the originator of the responseInfo.
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
        public DataResponseInfo<Guid> GenerateUuids(int numberOfItemsToReturn)
        {
            var parameters = UuidParameters.Create(numberOfItemsToReturn, _verifyOriginater);
            _verifyOriginater = false;

            var response = _methodCallBroker.Generate(parameters);
            return response as DataResponseInfo<Guid>;
        }

        /// <summary>
        /// Generates version 4 true random Universally Unique IDentifiers (UUIDs) in accordance with section 4.4 of RFC 4122
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random UUID values you need. Must be between 1 and 1000.</param>
        /// <returns>All information returned from random service, include the list of UUID values</returns>
        public async Task<DataResponseInfo<Guid>> GenerateUuidsAsync(int numberOfItemsToReturn)
        {
            var parameters = UuidParameters.Create(numberOfItemsToReturn, _verifyOriginater);
            _verifyOriginater = false;

            var response = await _methodCallBroker.GenerateAsync(parameters);
            return response as DataResponseInfo<Guid>;
        }
    }
}


