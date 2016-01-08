using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.Abstration
{
    /// <summary>
    /// Retrieve a lst of random guassian values
    /// </summary>
    public class GuassianMethod
    {
        private bool _verifyOriginater;
        private readonly IMethodCallBroker _methodCallBroker;

        /// <summary>
        /// Create an instance of <see cref="GuassianMethod"/>.  
        /// </summary>
        /// <param name="methodCallBroker">methodCallBroker class to use to retrieve blob information.  Default is <see cref="MethodCallBroker{T}"/></param>
        public GuassianMethod(IMethodCallBroker methodCallBroker = null)
        {
            _methodCallBroker = methodCallBroker ?? new MethodCallBroker<decimal>();
        }

        /// <summary>
        /// Verify the originator of the responseInfo.
        /// </summary>
        /// <example>
        /// new GuassianMethod().WithVerification().GenerateGuassians(...);
        /// </example>
        public GuassianMethod WithVerification()
        {
            _verifyOriginater = true;
            return this;
        }

        /// <summary>
        /// Retrieve a list of random guassian values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random guassian values you need. Must be between 1 and 10,000.</param>
        /// <param name="mean">The distribution's mean. Must be between -1,000,000 and 1,000,000.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must be between -1,000,000 and 1,000,000</param>
        /// <param name="significantDigits">The number of significant digits to use. Must be between 2 and 20.</param>
        /// <returns>All information returned from random service, include the list of guassian values</returns>
        public DataResponseInfo<decimal> GenerateGuassians(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            var parameters = GuassianParameters.Create(numberOfItemsToReturn, mean, standardDeviation, significantDigits, _verifyOriginater);
            _verifyOriginater = false;

            var response = _methodCallBroker.Generate(parameters);
            return response as DataResponseInfo<decimal>;
        }

        /// <summary>
        /// Retrieve a list of random guassian values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random guassian values you need. Must be between 1 and 10,000.</param>
        /// <param name="mean">The distribution's mean. Must be between -1,000,000 and 1,000,000.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must be between -1,000,000 and 1,000,000</param>
        /// <param name="significantDigits">The number of significant digits to use. Must be between 2 and 20.</param>
        /// <returns>All information returned from random service, include the list of guassian values</returns>
        public async Task<DataResponseInfo<decimal>> GenerateGuassiansAsync(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            var parameters = GuassianParameters.Create(numberOfItemsToReturn, mean, standardDeviation, significantDigits, _verifyOriginater);
            _verifyOriginater = false;

            var response = await _methodCallBroker.GenerateAsync(parameters);
            return response as DataResponseInfo<decimal>;
        }
    }
}


