using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    /// <summary>
    /// Retrieve a lst of random integer values
    /// </summary>
    public class StringBasicMethod
    {
        private readonly IBasicMethodManager<string> _basicMethodManager;

        /// <summary>
        /// Create an instance of <see cref="StringBasicMethod"/>.  
        /// </summary>
        /// <param name="basicMethodManager">basicMethodManager class to use to retrieve string information.  Default is <see cref="basicMethodManagerManager{T}"/></param>
        public StringBasicMethod(IBasicMethodManager<string> basicMethodManager = null)
        {
            _basicMethodManager = basicMethodManager ?? new BasicMethodManager<string>();
        }

        /// <summary>
        /// Retrieve a list of random integer values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random string values you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">Create of common character sets that are allowed to occur in the random strings</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of string values</returns>
        public IBasicMethodResponse<string> GenerateStrings(int numberOfItemsToReturn, int length, CharactersAllowed charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = StringParameters.Create(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates);

            var response = _basicMethodManager.Generate(parameters);
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
        public IBasicMethodResponse<string> GenerateStrings(int numberOfItemsToReturn, int length, string charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = StringParameters.Create(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates);

            var response = _basicMethodManager.Generate(parameters);
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
        public async Task<IBasicMethodResponse<string>> GenerateStringsAsync(int numberOfItemsToReturn, int length, CharactersAllowed charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = StringParameters.Create(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates);

            var response = await _basicMethodManager.GenerateAsync(parameters);
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
        public async Task<IBasicMethodResponse<string>> GenerateStringsAsync(int numberOfItemsToReturn, int length, string charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = StringParameters.Create(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates);

            var response = await _basicMethodManager.GenerateAsync(parameters);
            return response;
        }
    }
}


