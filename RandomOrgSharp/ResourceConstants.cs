namespace Obacher.RandomOrgSharp
{
    public static class ResourceConstants
    {
        private const string VALIDATION_KEY = "VALIDATION_";

        public const string APIKEY_REQUIRED = VALIDATION_KEY + "APIKEY_REQUIRED";
        public const string NUMBER_ITEMS_RETURNED_OUT_OF_RANGE = VALIDATION_KEY + "NUMBER_ITEMS_RETURNED_OUT_OF_RANGE";
        public const string MINIMUM_VALUE_OUT_OF_RANGE = VALIDATION_KEY + "MINIMUM_VALUE_OUT_OF_RANGE";
        public const string MAXIMUM_VALUE_OUT_OF_RANGE = VALIDATION_KEY + "MAXIMUM_VALUE_OUT_OF_RANGE";
    }
}
