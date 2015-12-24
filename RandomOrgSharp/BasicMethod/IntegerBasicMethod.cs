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
        private readonly IBasicMethod<int> _basicMethod;

        /// <summary>
        /// Create an instance of <see cref="IntegerBasicMethod"/>.  
        /// </summary>
        /// <param name="basicMethod">BasicMethod class to use to retrieve blob information.  Default is <see cref="BasicMethod{T}"/></param>
        public IntegerBasicMethod(IBasicMethod<int> basicMethod = null)
        {
            if (basicMethod == null)
                _basicMethod = new BasicMethod<int>(new RandomOrgApiService(), new MethodCallManager(), new JsonRequestBuilder(), new BasicMethodResponseParser<int>());

            _basicMethod = basicMethod;
        }

        /// <summary>
        /// Retrieve a list of random integer values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random integer values you need. Must be between 1 and 10,000.</param>
        /// <param name="minimumValue">The lower boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000 and 1,000,000,000.</param>
        /// <param name="maximumValue">The upper boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000a and 1,000,000,000.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <param name="baseNumber">Specifies the base that will be used to display the numbers, default to Base 10</param>
        /// <returns>All information returned from random service, include the list of integer values</returns>
        public IBasicMethodResponse<int> GenerateIntegers(int numberOfItemsToReturn, int minimumValue, int maximumValue,
            bool allowDuplicates = true, BaseNumberOptions baseNumber = BaseNumberOptions.Ten)
        {
            var parameters = IntegerParameters.Set(numberOfItemsToReturn, minimumValue, maximumValue, allowDuplicates, baseNumber);

            var response = _basicMethod.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Retrieve a list of random integer values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random integer values you need. Must be between 1 and 10,000.</param>
        /// <param name="minimumValue">The lower boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000 and 1,000,000,000.</param>
        /// <param name="maximumValue">The upper boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000a and 1,000,000,000.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <param name="baseNumber">Specifies the base that will be used to display the numbers, default to Base 10</param>
        /// <returns>All information returned from random service, include the list of integer values</returns>
        public async Task<IBasicMethodResponse<int>> GenerateIntegersAsync(int numberOfItemsToReturn, int minimumValue, int maximumValue,
            bool allowDuplicates = true, BaseNumberOptions baseNumber = BaseNumberOptions.Ten)
        {
            var parameters = IntegerParameters.Set(numberOfItemsToReturn, minimumValue, maximumValue, allowDuplicates, baseNumber);

            var response = await _basicMethod.GenerateAsync(parameters);
            return response;
        }
    }
}


