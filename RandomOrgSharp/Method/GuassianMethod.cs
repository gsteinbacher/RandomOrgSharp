using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.Method
{
    /// <summary>
    /// Retrieve a lst of random guassian values
    /// </summary>
    public class GuassianMethod
    {
        private bool _verifyOriginater;
        private readonly IDataMethodManager<decimal> _dataMethodManager;

        /// <summary>
        /// Create an instance of <see cref="GuassianMethod"/>.  
        /// </summary>
        /// <param name="dataMethodManager">dataMethodManager class to use to retrieve blob information.  Default is <see cref="DataMethodManager{T}"/></param>
        public GuassianMethod(IDataMethodManager<decimal> dataMethodManager = null)
        {
            _dataMethodManager = dataMethodManager ?? new DataMethodManager<decimal>();
        }

        /// <summary>
        /// Verify the originator of the response.
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
        public DataResponse<decimal> GenerateGuassians(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            var parameters = GuassianParameters.Create(numberOfItemsToReturn, mean, standardDeviation, significantDigits, _verifyOriginater);
            _verifyOriginater = false;

            var response = _dataMethodManager.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Retrieve a list of random guassian values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random guassian values you need. Must be between 1 and 10,000.</param>
        /// <param name="mean">The distribution's mean. Must be between -1,000,000 and 1,000,000.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must be between -1,000,000 and 1,000,000</param>
        /// <param name="significantDigits">The number of significant digits to use. Must be between 2 and 20.</param>
        /// <returns>All information returned from random service, include the list of guassian values</returns>
        public async Task<DataResponse<decimal>> GenerateGuassiansAsync(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            var parameters = GuassianParameters.Create(numberOfItemsToReturn, mean, standardDeviation, significantDigits, _verifyOriginater);
            _verifyOriginater = false;

            var response = await _dataMethodManager.GenerateAsync(parameters);
            return response;
        }
    }
}


