namespace Obacher.RandomOrgSharp
{
    public static class StringsConstants
    {
        private const string VALIDATION_KEY = "VALIDATION_";
        private const string EXCEPTION_KEY = "EXCEPTION_";

        public const string ERROR_CODE_KEY = "ERROR_CODE_";

        #region Validation messages

        public const string APIKEY_REQUIRED = VALIDATION_KEY + "APIKEY_REQUIRED";
        public const string NUMBER_ITEMS_RETURNED_OUT_OF_RANGE = VALIDATION_KEY + "NUMBER_ITEMS_RETURNED_OUT_OF_RANGE";
        public const string MINIMUM_VALUE_OUT_OF_RANGE = VALIDATION_KEY + "MINIMUM_VALUE_OUT_OF_RANGE";
        public const string MAXIMUM_VALUE_OUT_OF_RANGE = VALIDATION_KEY + "MAXIMUM_VALUE_OUT_OF_RANGE";

        public const string DECIMAL_PLACES_VALUE_OUT_OF_RANGE = VALIDATION_KEY + "DECIMAL_PLACES_VALUE_OUT_OF_RANGE";

        public const string MEAN_VALUE_OUT_OF_RANGE = VALIDATION_KEY + "MEAN_VALUE_OUT_OF_RANGE";
        public const string STANDARD_DEVIATION_OUT_OF_RANGE = VALIDATION_KEY + "STANDARD_DEVIATION_OUT_OF_RANGE";
        public const string SIGNIFICANT_DIGITS_OUT_OF_RANGE = VALIDATION_KEY + "SIGNIFICANT_DIGITS_OUT_OF_RANGE";

        public const string STRING_LENGTH_OUT_OF_RANGE = VALIDATION_KEY + "STRING_LENGTH_OUT_OF_RANGE";
        public const string CHARACTERS_ALLOWED_OUT_OF_RANGE = VALIDATION_KEY + "CHARACTERS_ALLOWED_OUT_OF_RANGE";

        public const string BLOB_SIZE_OUT_OF_RANGE = VALIDATION_KEY + "BLOB_SIZE_OUT_OF_RANGE";
        public const string BLOB_SIZE_NOT_DIVISIBLE_BY_8 = VALIDATION_KEY + "BLOB_SIZE_NOT_DIVISIBLE_BY_8";

        public const string IDS_NOT_MATCHED = "ERROR_IDS_NOT_MATCHED";

        #endregion

        #region Exception messages

        public const string EXCEPTION_INVALID_ARGUMENT = EXCEPTION_KEY + "INVALID_ARGUMENT";

        #endregion
    }
}
