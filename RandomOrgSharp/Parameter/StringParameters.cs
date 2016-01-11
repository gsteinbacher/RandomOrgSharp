using Obacher.Framework.Common;

namespace Obacher.RandomOrgSharp.Core.Parameter
{
    public enum CharactersAllowed
    {
        Alpha,
        UpperOnly,
        LowerOnly,
        AlphaNumeric,
        UpperNumeric,
        LowerNumeric
    }

    /// <summary>
    /// Class which contains the parameters used when requesting random string values from random.org
    /// </summary>
    public sealed class StringParameters : CommonParameters
    {
        private const int MaxItemsAllowed = 10000;

        public int NumberOfItemsToReturn { get; private set; }
        public int Length { get; private set; }
        public string CharactersAllowed { get; private set; }
        public bool AllowDuplicates { get; private set; }

        /// <summary>
        /// Constructor used to pass information to the <see cref="CommonParameters"/> base class
        /// </summary>
        /// <param name="method">Method to call at random.org</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        private StringParameters(MethodType method, bool verifyOriginator) : base(method, verifyOriginator) { }

        /// <summary>
        /// Build an instance of <see cref="StringParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random string values you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">Build of common character sets that are allowed to occur in the random strings</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        /// <returns>Instance of <see cref="StringParameters"/></returns>
        public static StringParameters Create(int numberOfItemsToReturn, int length, CharactersAllowed charactersAllowed, bool allowDuplicates = true, bool verifyOriginator = false)
        {
            var parameters = new StringParameters(MethodType.String, verifyOriginator);
            parameters.SetAllowedParameter(charactersAllowed);
            parameters.SetParameters(numberOfItemsToReturn, length, allowDuplicates);
            return parameters;
        }

        /// <summary>
        /// Build an instance of <see cref="StringParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">A string that contains the set of characters that are allowed to occur in the random strings. The maximum number of characters is 80.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        /// <returns>Instance of <see cref="StringParameters"/></returns>
        public static StringParameters Create(int numberOfItemsToReturn, int length, string charactersAllowed, bool allowDuplicates = true, bool verifyOriginator = false)
        {
            var parameters = new StringParameters(MethodType.String, verifyOriginator);
            parameters.SetAllowedParameter(charactersAllowed);
            parameters.SetParameters(numberOfItemsToReturn, length, allowDuplicates);
            return parameters;
        }

        private void SetAllowedParameter(CharactersAllowed charactersAllowed)
        {
            switch (charactersAllowed)
            {
                case Parameter.CharactersAllowed.Alpha:
                    CharactersAllowed = RandomOrgConstants.CHARACTERS_ALLOWED_ALPHA;
                    break;

                case Parameter.CharactersAllowed.UpperOnly:
                    CharactersAllowed = RandomOrgConstants.CHARACTERS_ALLOWED_UPPER_ONLY;
                    break;

                case Parameter.CharactersAllowed.LowerOnly:
                    CharactersAllowed = RandomOrgConstants.CHARACTERS_ALLOWED_LOWER_ONLY;
                    break;

                case Parameter.CharactersAllowed.UpperNumeric:
                    CharactersAllowed = RandomOrgConstants.CHARACTERS_ALLOWED_UPPER_NUMERIC;
                    break;

                case Parameter.CharactersAllowed.LowerNumeric:
                    CharactersAllowed = RandomOrgConstants.CHARACTERS_ALLOWED_LOWER_NUMERIC;
                    break;

                default:
                    CharactersAllowed = RandomOrgConstants.CHARACTERS_ALLOWED_ALPHA_NUMERIC;
                    break;
            }
        }

        private void SetAllowedParameter(string charactersAllowed)
        {
            if (charactersAllowed == null)
                charactersAllowed = string.Empty;

            if (!charactersAllowed.Length.Between(1, 80))
                throw new RandomOrgRunTimeException(
                    ResourceHelper.GetString(StringsConstants.CHARACTERS_ALLOWED_OUT_OF_RANGE));

            CharactersAllowed = charactersAllowed;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters(int numberOfItemsToReturn, int length, bool allowDuplicates)
        {
            if (!numberOfItemsToReturn.Between(1, MaxItemsAllowed))
                throw new RandomOrgRunTimeException(
                    ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MaxItemsAllowed));

            if (!length.Between(1, 20))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.STRING_LENGTH_OUT_OF_RANGE));

            NumberOfItemsToReturn = numberOfItemsToReturn;
            Length = length;
            AllowDuplicates = allowDuplicates;
        }
    }
}
