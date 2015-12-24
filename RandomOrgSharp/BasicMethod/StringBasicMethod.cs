﻿using System.Threading.Tasks;
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
        private readonly IBasicMethod<string> _basicMethod;

        /// <summary>
        /// Create an instance of <see cref="StringBasicMethod"/>.  
        /// </summary>
        /// <param name="basicMethod">BasicMethod class to use to retrieve string information.  Default is <see cref="BasicMethod{T}"/></param>
        public StringBasicMethod(IBasicMethod<string> basicMethod = null)
        {
            if (basicMethod == null)
                _basicMethod = new BasicMethod<string>(new RandomOrgApiService(), new MethodCallManager(), new JsonRequestBuilder(), new BasicMethodResponseParser<string>());

            _basicMethod = basicMethod;
        }

        /// <summary>
        /// Retrieve a list of random integer values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random string values you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">Set of common character sets that are allowed to occur in the random strings</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of string values</returns>
        public IBasicMethodResponse<string> GenerateStrings(int numberOfItemsToReturn, int length, CharactersAllowed charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = StringParameters.Set(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates);

            var response = _basicMethod.Generate(parameters);
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
            var parameters = StringParameters.Set(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates);

            var response = _basicMethod.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Retrieve a list of random integer values as an asynchronous operation
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random string values you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">Set of common character sets that are allowed to occur in the random strings</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>All information returned from random service, include the list of guassian values</returns>
        public async Task<IBasicMethodResponse<string>> GenerateStringsAsync(int numberOfItemsToReturn, int length, CharactersAllowed charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = StringParameters.Set(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates);

            var response = await _basicMethod.GenerateAsync(parameters);
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
            var parameters = StringParameters.Set(numberOfItemsToReturn, length, charactersAllowed, allowDuplicates);

            var response = await _basicMethod.GenerateAsync(parameters);
            return response;
        }
    }
}

