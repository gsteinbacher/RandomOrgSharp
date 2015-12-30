using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.Method
{
    /// <summary>
    /// Retrieve a lst of random integer values
    /// </summary>
    public class StringMethod
    {
        private bool _verifyOriginater;
        private readonly IDataMethodManager<string> _dataMethodManager;

        /// <summary>
        /// Create an instance of <see cref="StringMethod"/>.  
        /// </summary>
        /// <param name="dataMethodManager">dataMethodManager class to use to retrieve string information.  Default is <see cref="DataMethodManager{T}"/></param>
        public StringMethod(IDataMethodManager<string> dataMethodManager = null)
        {
            _dataMethodManager = dataMethodManager ?? new DataMethodManager<string>();
        }

        /// <summary>
        /// Verify the originator of the response.
        /// </summary>
        /// <example>
        /// new StringMethod().WithVerification().GenerateStrings(...);
        /// </example>
        public StringMethod WithVerification()
        {
            _verifyOriginater = true;
            return this;
        }

        /// <summary>
        /// Retrieve a list of random integer values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random string values you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">Create of common character sets that are allowed to occur in the random strings</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of string values</returns>
        public DataResponse<string> GenerateStrings(int numberOfItemsToReturn, int length, CharactersAllowed charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = StringParameters.Create(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates, _verifyOriginater);
            _verifyOriginater = false;

            var response = _dataMethodManager.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Retrieve a list of random integer values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random string values you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">A string that contains the set of characters that are allowed to occur in the random strings. The maximum number of characters is 80.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of string values</returns>
        public DataResponse<string> GenerateStrings(int numberOfItemsToReturn, int length, string charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = StringParameters.Create(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates, _verifyOriginater);
            _verifyOriginater = false;

            var response = _dataMethodManager.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Retrieve a list of random integer values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random string values you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">Create of common character sets that are allowed to occur in the random strings</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of guassian values</returns>
        public async Task<DataResponse<string>> GenerateStringsAsync(int numberOfItemsToReturn, int length, CharactersAllowed charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = StringParameters.Create(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates, _verifyOriginater);
            _verifyOriginater = false;

            var response = await _dataMethodManager.GenerateAsync(parameters);
            return response;
        }

        /// <summary>
        /// Retrieve a list of random integer values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random string values you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">A string that contains the set of characters that are allowed to occur in the random strings. The maximum number of characters is 80.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of guassian values</returns>
        public async Task<DataResponse<string>> GenerateStringsAsync(int numberOfItemsToReturn, int length, string charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = StringParameters.Create(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates, _verifyOriginater);
            _verifyOriginater = false;

            var response = await _dataMethodManager.GenerateAsync(parameters);
            return response;
        }
    }
}


