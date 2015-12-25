using System;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    /// <summary>
    /// Retrieve a lst of random UUID values
    /// </summary>
    public class UuidBasicMethod
    {
        private readonly IBasicMethod<Guid> _basicMethod;

        /// <summary>
        /// Create an instance of <see cref="UuidBasicMethod"/>.  
        /// </summary>
        /// <param name="basicMethod">BasicMethod class to use to retrieve string information.  Default is <see cref="BasicMethod{T}"/></param>
        public UuidBasicMethod(IBasicMethod<Guid> basicMethod = null)
        {
            _basicMethod = basicMethod ?? new BasicMethod<Guid>();
        }

        /// <summary>
        /// Generates version 4 true random Universally Unique IDentifiers (UUIDs) in accordance with section 4.4 of RFC 4122
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random UUID values you need. Must be between 1 and 1000.</param>
        /// <returns>All information returned from random service, include the list of UUID values</returns>
        public IBasicMethodResponse<Guid> GenerateUuids(int numberOfItemsToReturn)
        {
            var parameters = UuidParameters.Create(numberOfItemsToReturn);

            var response = _basicMethod.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Generates version 4 true random Universally Unique IDentifiers (UUIDs) in accordance with section 4.4 of RFC 4122
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random UUID values you need. Must be between 1 and 1000.</param>
        /// <returns>All information returned from random service, include the list of UUID values</returns>
        public async Task<IBasicMethodResponse<Guid>> GenerateUuidsAsync(int numberOfItemsToReturn)
        {
            var parameters = UuidParameters.Create(numberOfItemsToReturn);

            var response = await _basicMethod.GenerateAsync(parameters);
            return response;
        }
    }
}


