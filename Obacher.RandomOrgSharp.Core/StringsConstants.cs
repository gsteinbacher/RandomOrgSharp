namespace Obacher.RandomOrgSharp.Core
{
    public static class StringsConstants
    {
        private const string ValidationKey = "VALIDATION_";
        private const string ExceptionKey = "EXCEPTION_";

        public const string ERROR_CODE_KEY = "ERROR_CODE_";
        public const string ERROR_CODE_100 = "ERROR_CODE_100";

        #region Validation messages

        public const string APIKEY_REQUIRED = ValidationKey + "APIKEY_REQUIRED";
        public const string NUMBER_ITEMS_RETURNED_OUT_OF_RANGE = ValidationKey + "NUMBER_ITEMS_RETURNED_OUT_OF_RANGE";
        public const string MINIMUM_VALUE_OUT_OF_RANGE = ValidationKey + "MINIMUM_VALUE_OUT_OF_RANGE";
        public const string MAXIMUM_VALUE_OUT_OF_RANGE = ValidationKey + "MAXIMUM_VALUE_OUT_OF_RANGE";

        public const string DECIMAL_PLACES_VALUE_OUT_OF_RANGE = ValidationKey + "DECIMAL_PLACES_VALUE_OUT_OF_RANGE";

        public const string MEAN_VALUE_OUT_OF_RANGE = ValidationKey + "MEAN_VALUE_OUT_OF_RANGE";
        public const string STANDARD_DEVIATION_OUT_OF_RANGE = ValidationKey + "STANDARD_DEVIATION_OUT_OF_RANGE";
        public const string SIGNIFICANT_DIGITS_OUT_OF_RANGE = ValidationKey + "SIGNIFICANT_DIGITS_OUT_OF_RANGE";

        public const string STRING_LENGTH_OUT_OF_RANGE = ValidationKey + "STRING_LENGTH_OUT_OF_RANGE";
        public const string CHARACTERS_ALLOWED_OUT_OF_RANGE = ValidationKey + "CHARACTERS_ALLOWED_OUT_OF_RANGE";

        public const string BLOB_SIZE_OUT_OF_RANGE = ValidationKey + "BLOB_SIZE_OUT_OF_RANGE";
        public const string BLOB_SIZE_NOT_DIVISIBLE_BY_8 = ValidationKey + "BLOB_SIZE_NOT_DIVISIBLE_BY_8";

        public const string RESULT_REQUIRED = ValidationKey + "RESULT_REQUIRED";
        public const string SIGNATURE_REQUIRED = ValidationKey + "SIGNATURE_REQUIRED";

        public const string IDS_NOT_MATCHED = "ERROR_IDS_NOT_MATCHED";

        #endregion

        #region Exception messages

        public const string EXCEPTION_INVALID_ARGUMENT = ExceptionKey + "INVALID_ARGUMENT";
        public const string NOT_VERIFIED = ExceptionKey + "NOT_VERIFIED";
        public const string EXCEPTION_CANNOT_BE_NULLOREMPTY = ExceptionKey + "CANNOT_BE_NULLOREMPTY";

        #endregion
    }
}
