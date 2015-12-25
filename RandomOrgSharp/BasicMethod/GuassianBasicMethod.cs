using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    /// <summary>
    /// Retrieve a lst of random guassian values
    /// </summary>
    public class GuassianBasicMethod
    {
        private readonly IBasicMethod<decimal> _basicMethod;

        /// <summary>
        /// Create an instance of <see cref="GuassianBasicMethod"/>.  
        /// </summary>
        /// <param name="basicMethod">BasicMethod class to use to retrieve blob information.  Default is <see cref="BasicMethod{T}"/></param>
        public GuassianBasicMethod(IBasicMethod<decimal> basicMethod = null)
        {
            _basicMethod = basicMethod ?? new BasicMethod<decimal>();
        }

        /// <summary>
        /// Retrieve a list of random guassian values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random guassian values you need. Must be between 1 and 10,000.</param>
        /// <param name="mean">The distribution's mean. Must be between -1,000,000 and 1,000,000.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must be between -1,000,000 and 1,000,000</param>
        /// <param name="significantDigits">The number of significant digits to use. Must be between 2 and 20.</param>
        /// <returns>All information returned from random service, include the list of guassian values</returns>
        public IBasicMethodResponse<decimal> GenerateGuassians(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            var parameters = GuassianParameters.Create(numberOfItemsToReturn, mean, standardDeviation, significantDigits);

            var response = _basicMethod.Generate(parameters);
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
        public async Task<IBasicMethodResponse<decimal>> GenerateGuassiansAsync(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            var parameters = GuassianParameters.Create(numberOfItemsToReturn, mean, standardDeviation, significantDigits);

            var response = await _basicMethod.GenerateAsync(parameters);
            return response;
        }
    }
}


