namespace Obacher.RandomOrgSharp.JsonRPC
{
    internal static class JsonRpcConstants
    {
        public const string APIKEY_KEY = "apiKey";
        public const string APIKEY_VALUE = "a0fc5d8d-8812-4093-851d-5bf64b4310d0";

        public const string INTEGER_METHOD = "generateIntegers";
        public const string DECIMAL_METHOD = "generateDecimalFractions";
        public const string GAUSSIAN_METHOD = "generateGaussians";
        public const string STRING_METHOD = "generateStrings";
        public const string UUID_METHOD = "generateUUIDs";
        public const string BLOB_METHOD = "generateBlobs";
        public const string INTEGER_SIGNED_METHOD = "generateSignedIntegers";
        public const string DECIMAL_SIGNED_METHOD = "generateSignedDecimalFractions";
        public const string GAUSSIAN_SIGNED_METHOD = "generateSignedGaussians";
        public const string STRING_SIGNED_METHOD = "generateSignedStrings";
        public const string UUID_SIGNED_METHOD = "generateSignedUUIDs";
        public const string BLOB_SIGNED_METHOD = "generateSignedBlobs";

        public const string USAGE_METHOD = "getUsage";
        public const string VERIFY_SIGNATURE_METHOD = "verifySignature";

        // Parameter names that are used in the JSON request object.
        public const string RPC_PARAMETER_NAME = "jsonrpc";
        public const string RPC_VALUE = "2.0";
        public const string METHOD_PARAMETER_NAME = "method";
        public const string PARAMETERS_PARAMETER_NAME = "params";
        public const string ID_PARAMETER_NAME = "id";

        // General method parameters
        public const string NUMBER_ITEMS_RETURNED_PARAMETER_NAME = "n";
        public const string REPLACEMENT_PARAMETER_NAME = "replacement";
        public const string BASE_NUMBER_PARAMETER_NAME = "base";

        // Integer method specific parameters
        public const string MINIMUM_VALUE_PARAMETER_NAME = "min";
        public const string MAXIMUM_VALUE_PARAMETER_NAME = "max";

        // Decimal method specific parameters
        public const string DECIMAL_PLACES_PARAMETER_NAME = "decimalPlaces";

        // Guassian method specific parameters
        public const string MEAN_PARAMETER_NAME = "mean";
        public const string STANDARD_DEVIATION_PARAMETER_NAME = "standardDeviation";
        public const string SIGNIFICANT_DIGITS_PARAMETER_NAME = "significantDigits";

        // String method specific parameters
        public const string LENGTH_PARAMETER_NAME = "length";
        public const string CHARACTERS_ALLOWED_PARAMETER_NAME = "characters";

        // Blob method specific parameters
        public const string SIZE_PARAMETER_NAME = "size";
        public const string FORMAT_PARAMETER_NAME = "format";

        // Parameter names that are used in the JSON response object.
        public const string RESULT_PARAMETER_NAME = "result";
        public const string RANDOM_PARAMETER_NAME = "random";
        public const string DATA_PARAMETER_NAME = "data";
        public const string COMPLETION_TIME_PARAMETER_NAME = "completionTime";
        public const string BITS_USED_PARAMETER_NAME = "bitsUsed";
        public const string STATUS_PARAMETER_NAME = "status";
        public const string CREATION_TIME_PARAMETER_NAME = "creationTime";
        public const string ADVISORY_DELAY_PARAMETER_NAME = "advisoryDelay";
        public const string BITS_LEFT_PARAMETER_NAME = "bitsLeft";
        public const string REQUESTS_LEFT_PARAMETER_NAME = "requestsLeft";
        public const string TOTAL_BITS_PARAMETER_NAME = "totalBits";
        public const string TOTAL_REQUESTS_PARAMETER_NAME = "totalRequests";
        public const string SIGNATURE_PARAMETER_NAME = "signature";
        public const string AUTHENTICITY_PARAMETER_NAME = "authenticity";

        // Parameter names that are used in the JSON error response object
        public const string ERROR_PARAMETER_NAME = "error";
        public const string CODE_PARAMETER_NAME = "code";
        public const string MESSAGE_PARAMETER_NAME = "message";
    }
}