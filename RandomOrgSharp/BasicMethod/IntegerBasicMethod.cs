using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    /// <summary>
    /// Retrieve a lst of random integer values
    /// </summary>
    public class IntegerBasicMethod
    {
        private readonly IBasicMethodManager<int> _basicMethodManager;

        /// <summary>
        /// Create an instance of <see cref="IntegerBasicMethod"/>.  
        /// </summary>
        /// <param name="basicMethodManager">basicMethodManager class to use to retrieve blob information.  Default is <see cref="basicMethodManagerManager{T}"/></param>
        public IntegerBasicMethod(IBasicMethodManager<int> basicMethodManager = null)
        {
            _basicMethodManager = basicMethodManager ?? new BasicMethodManager<int>();
        }

        /// <summary>
        /// Retrieve a list of random integer values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random integer values you need. Must be between 1 and 10,000.</param>
        /// <param name="minimumValue">The lower boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000 and 1,000,000,000.</param>
        /// <param name="maximumValue">The upper boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000a and 1,000,000,000.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of integer values</returns>
        public IBasicMethodResponse<int> GenerateIntegers(int numberOfItemsToReturn, int minimumValue, int maximumValue, bool allowDuplicates = true)
        {
            var parameters = IntegerParameters.Create(numberOfItemsToReturn, minimumValue, maximumValue, allowDuplicates);

            var response = _basicMethodManager.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Retrieve a list of random integer values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random integer values you need. Must be between 1 and 10,000.</param>
        /// <param name="minimumValue">The lower boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000 and 1,000,000,000.</param>
        /// <param name="maximumValue">The upper boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000a and 1,000,000,000.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of integer values</returns>
        public async Task<IBasicMethodResponse<int>> GenerateIntegersAsync(int numberOfItemsToReturn, int minimumValue, int maximumValue, bool allowDuplicates = true)
        {
            var parameters = IntegerParameters.Create(numberOfItemsToReturn, minimumValue, maximumValue, allowDuplicates);

            var response = await _basicMethodManager.GenerateAsync(parameters);
            return response;
        }
    }
}


