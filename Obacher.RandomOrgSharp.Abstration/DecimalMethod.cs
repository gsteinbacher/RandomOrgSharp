using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.Abstration
{
    /// <summary>
    /// Retrieve a lst of random decimal values
    /// </summary>
    public class DecimalMethod
    {
        private bool _verifyOriginater;
        private readonly IMethodCallBroker _methodCallBroker;

        /// <summary>
        /// Create an instance of <see cref="DecimalMethod"/>.  
        /// </summary>
        /// <param name="methodCallBroker">methodCallBroker class to use to retrieve decimal information.  Default is <see cref="MethodCallBroker{T}"/></param>
        public DecimalMethod(IMethodCallBroker methodCallBroker = null)
        {
            _methodCallBroker = methodCallBroker ?? new MethodCallBroker<decimal>();
        }

        /// <summary>
        /// Verify the originator of the responseInfo.
        /// </summary>
        /// <example>
        /// new DecimalMethod().WithVerification().GenerateDecimalFractions(...);
        /// </example>
        public DecimalMethod WithVerification()
        {
            _verifyOriginater = true;
            return this;
        }

        /// <summary>
        /// Retrieve a list of random decimal values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <param name="numberOfDecimalPlaces">The number of decimal places to use. Must be between 1 and 20</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of decimal values</returns>
        public DataResponseInfo<decimal> GenerateDecimalFractions(int numberOfItemsToReturn, int numberOfDecimalPlaces, bool allowDuplicates = true)
        {
            var parameters = DecimalParameters.Create(numberOfItemsToReturn, numberOfDecimalPlaces, allowDuplicates, _verifyOriginater);
            _verifyOriginater = false;

            var response = _methodCallBroker.Generate(parameters);
            return response as DataResponseInfo<decimal>;
        }

        /// <summary>
        /// Retrieve a list of random decimal values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <param name="numberOfDecimalPlaces">The number of decimal places to use. Must be between 1 and 20</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of decimal values</returns>
        public async Task<DataResponseInfo<decimal>> GenerateDecimalFractionsAsync(int numberOfItemsToReturn, int numberOfDecimalPlaces, bool allowDuplicates = true)
        {
            var parameters = DecimalParameters.Create(numberOfItemsToReturn, numberOfDecimalPlaces, allowDuplicates, _verifyOriginater);
            _verifyOriginater = false;

            var response = await _methodCallBroker.GenerateAsync(parameters);
            return response as DataResponseInfo<decimal>;
        }
    }
}
