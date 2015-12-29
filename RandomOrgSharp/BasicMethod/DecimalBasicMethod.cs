using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    /// <summary>
    /// Retrieve a lst of random decimal values
    /// </summary>
    public class DecimalBasicMethod
    {
        private readonly IBasicMethodManager<decimal> _basicMethodManager;

        /// <summary>
        /// Create an instance of <see cref="DecimalBasicMethod"/>.  
        /// </summary>
        /// <param name="basicMethodManager">basicMethodManager class to use to retrieve decimal information.  Default is <see cref="basicMethodManagerManager{T}"/></param>
        public DecimalBasicMethod(IBasicMethodManager<decimal> basicMethodManager = null)
        {
            _basicMethodManager = basicMethodManager ?? new BasicMethodManager<decimal>();
        }

        /// <summary>
        /// Retrieve a list of random decimal values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <param name="numberOfDecimalPlaces">The number of decimal places to use. Must be between 1 and 20</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of decimal values</returns>
        public IBasicMethodResponse<decimal> GenerateDecimalFractions(int numberOfItemsToReturn, int numberOfDecimalPlaces, bool allowDuplicates = true)
        {
            var parameters = DecimalParameters.Create(numberOfItemsToReturn, numberOfDecimalPlaces, allowDuplicates);

            var response = _basicMethodManager.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Retrieve a list of random decimal values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <param name="numberOfDecimalPlaces">The number of decimal places to use. Must be between 1 and 20</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of decimal values</returns>
        public async Task<IBasicMethodResponse<decimal>> GenerateDecimalFractionsAsync(int numberOfItemsToReturn, int numberOfDecimalPlaces, bool allowDuplicates = true)
        {
            var parameters = DecimalParameters.Create(numberOfItemsToReturn, numberOfDecimalPlaces, allowDuplicates);

            var response = await _basicMethodManager.GenerateAsync(parameters);
            return response;
        }
    }
}
