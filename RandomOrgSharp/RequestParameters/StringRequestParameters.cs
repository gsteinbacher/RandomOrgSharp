using Newtonsoft.Json.Linq;
using Obacher.Framework.Common;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.RequestParameters
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

    public class StringRequestParameters : CommonRequestParameters
    {
        private readonly int _numberOfItemsToReturn;
        private readonly int _length;
        private readonly string _charactersAllowed;
        private readonly bool _allowDuplicates;


        public StringRequestParameters(int numberOfItemsToReturn, int length, CharactersAllowed charactersAllowed,
            bool allowDuplicates = true) : this(numberOfItemsToReturn, length, "a", allowDuplicates)
        {
            switch (charactersAllowed)
            {
                case CharactersAllowed.Alpha:
                    _charactersAllowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    break;

                case CharactersAllowed.UpperOnly:
                    _charactersAllowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;

                case CharactersAllowed.LowerOnly:
                    _charactersAllowed = "abcdefghijklmnopqrstuvwxyz";
                    break;

                case CharactersAllowed.UpperNumeric:
                    _charactersAllowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    break;

                case CharactersAllowed.LowerNumeric:
                    _charactersAllowed = "abcdefghijklmnopqrstuvwxyz0123456789";
                    break;

                default:
                    _charactersAllowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    break;
            }
        }

        public StringRequestParameters(int numberOfItemsToReturn, int length, string charactersAllowed, bool allowDuplicates = true)
        {
            if (!numberOfItemsToReturn.Between(1, 10000))
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE));

            if (!length.Between(1, 20))
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.STRING_LENGTH_OUT_OF_RANGE));

            if (charactersAllowed == null)
                charactersAllowed = string.Empty;

            if (!charactersAllowed.Length.Between(1, 80))
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.CHARACTERS_ALLOWED_OUT_OF_RANGE));

            _numberOfItemsToReturn = numberOfItemsToReturn;
            _length = length;
            _charactersAllowed = charactersAllowed;
            _allowDuplicates = allowDuplicates;
        }

        public override JObject CreateJsonRequest()
        {
            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _numberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_LENGTH_PARAMETER_NAME, _length),
                new JProperty(RandomOrgConstants.JSON_CHARACTERS_ALLOWED_PARAMETER_NAME, _charactersAllowed),
                new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, _allowDuplicates)
                );

            JObject jsonRequest = CreateJsonRequestInternal(RandomOrgConstants.STRING_METHOD, jsonParameters);
            return jsonRequest;
        }
    }
}
