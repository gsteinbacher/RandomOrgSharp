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

    public class StringParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 10000;

        public int NumberOfItemsToReturn { get; private set; }
        public int Length { get; private set; }
        public string Allowed { get; private set; }
        public bool AllowDuplicates { get; private set; }

        public void SetParameters(int numberOfItemsToReturn, int length, CharactersAllowed charactersAllowed, bool allowDuplicates = true)
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


            SetParameters(numberOfItemsToReturn, length, allowDuplicates);
        }

        public void SetParameters(int numberOfItemsToReturn, int length, string charactersAllowed, bool allowDuplicates = true)
        {
            if (charactersAllowed == null)
                charactersAllowed = string.Empty;

            if (!charactersAllowed.Length.Between(1, 80))
                throw new RandomOrgRunTimeException(
                    ResourceHelper.GetString(StringsConstants.CHARACTERS_ALLOWED_OUT_OF_RANGE));

            Allowed = charactersAllowed;

            SetParameters(numberOfItemsToReturn, length, allowDuplicates);
        }

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

            Method = RandomOrgConstants.STRING_METHOD;
        }
    }
}
