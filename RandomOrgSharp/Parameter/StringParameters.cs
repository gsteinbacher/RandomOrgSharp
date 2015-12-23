using Obacher.Framework.Common;

namespace Obacher.RandomOrgSharp.Parameter
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
    /// Class which contains the parameters used when requesting random blob values from random.org
    /// </summary>
    public class StringParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 10000;

        public int NumberOfItemsToReturn { get; private set; }
        public int Length { get; private set; }
        public string Allowed { get; private set; }
        public bool AllowDuplicates { get; private set; }

        /// <summary>
        /// Create an instance of <see cref="StringParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">Set of common character sets that are allowed to occur in the random strings</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>Instance of <see cref="StringParameters"/></returns>
        public static StringParameters Set(int numberOfItemsToReturn, int length, CharactersAllowed charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = new StringParameters();
            parameters.SetAllowedParameter(charactersAllowed);
            parameters.SetParameters(numberOfItemsToReturn, length, allowDuplicates);
            return parameters;
        }

        /// <summary>
        /// Create an instance of <see cref="StringParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length</param>
        /// <param name="charactersAllowed">A string that contains the set of characters that are allowed to occur in the random strings. The maximum number of characters is 80.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>Instance of <see cref="StringParameters"/></returns>
        public static StringParameters Set(int numberOfItemsToReturn, int length, string charactersAllowed, bool allowDuplicates = true)
        {
            var parameters = new StringParameters();
            parameters.SetAllowedParameter(charactersAllowed);
            parameters.SetParameters(numberOfItemsToReturn, length, allowDuplicates);
            return parameters;
        }

        private void SetAllowedParameter(CharactersAllowed charactersAllowed)
        {
            switch (charactersAllowed)
            {
                case CharactersAllowed.Alpha:
                    Allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    break;

                case CharactersAllowed.UpperOnly:
                    Allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;

                case CharactersAllowed.LowerOnly:
                    Allowed = "abcdefghijklmnopqrstuvwxyz";
                    break;

                case CharactersAllowed.UpperNumeric:
                    Allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    break;

                case CharactersAllowed.LowerNumeric:
                    Allowed = "abcdefghijklmnopqrstuvwxyz0123456789";
                    break;

                default:
                    Allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
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

            Allowed = charactersAllowed;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters(int numberOfItemsToReturn, int length, bool allowDuplicates)
        {
            if (!numberOfItemsToReturn.Between(1, MAX_ITEMS_ALLOWED))
                throw new RandomOrgRunTimeException(
                    ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MAX_ITEMS_ALLOWED));

            if (!length.Between(1, 20))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.STRING_LENGTH_OUT_OF_RANGE));

            NumberOfItemsToReturn = numberOfItemsToReturn;
            Length = length;
            AllowDuplicates = allowDuplicates;

            MethodType = MethodType.String;
        }
    }
}
