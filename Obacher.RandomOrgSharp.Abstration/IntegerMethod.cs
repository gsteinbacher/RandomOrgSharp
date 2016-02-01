using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.Abstration
{
    /// <summary>
    /// Retrieve a lst of random integer values
    /// </summary>
    public class IntegerMethod
    {
        private bool _verifyOriginater;
        private readonly IMethodCallBroker _methodCallBroker;

        /// <summary>
        /// Create an instance of <see cref="IntegerMethod"/>.  
        /// </summary>
        /// <param name="methodCallBroker">methodCallBroker class to use to retrieve blob information.  Default is <see cref="MethodCallBroker{T}"/></param>
        public IntegerMethod(IMethodCallBroker methodCallBroker = null)
        {
            _methodCallBroker = methodCallBroker ?? new MethodCallBroker<int>();
        }

        /// <summary>
        /// Verify the originator of the responseInfo.
        /// </summary>
        /// <example>
        /// new IntegerMethod().WithVerification().GenerateIntegers(...);
        /// </example>
        public IntegerMethod WithVerification()
        {
            _verifyOriginater = true;
            return this;
        }

        /// <summary>
        /// Retrieve a list of random integer values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random integer values you need. Must be between 1 and 10,000.</param>
        /// <param name="minimumValue">The lower boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000 and 1,000,000,000.</param>
        /// <param name="maximumValue">The upper boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000a and 1,000,000,000.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of integer values</returns>
        public DataResponseInfo<int> GenerateIntegers(int numberOfItemsToReturn, int minimumValue, int maximumValue, bool allowDuplicates = true)
        {
            var parameters = IntegerParameters.Create(numberOfItemsToReturn, minimumValue, maximumValue, allowDuplicates, _verifyOriginater);
            _verifyOriginater = false;

            var response = _methodCallBroker.Generate(parameters);
            return response as DataResponseInfo<int>;
        }

        /// <summary>
        /// Retrieve a list of random integer values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random integer values you need. Must be between 1 and 10,000.</param>
        /// <param name="minimumValue">The lower boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000 and 1,000,000,000.</param>
        /// <param name="maximumValue">The upper boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000a and 1,000,000,000.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of integer values</returns>
        public async Task<DataResponseInfo<int>> GenerateIntegersAsync(int numberOfItemsToReturn, int minimumValue, int maximumValue, bool allowDuplicates = true)
        {
            var parameters = IntegerParameters.Create(numberOfItemsToReturn, minimumValue, maximumValue, allowDuplicates, _verifyOriginater);
            _verifyOriginater = false;

            var response = await _methodCallBroker.GenerateAsync(parameters);
            return response as DataResponseInfo<int>;
        }
    }
}


