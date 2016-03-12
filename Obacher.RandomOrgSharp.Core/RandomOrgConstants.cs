namespace Obacher.RandomOrgSharp.Core
{
    public static class RandomOrgConstants
    {

        public const string APIKEY_KEY = "apiKey";

        // Methods to call to generate random values
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

        // Possible status values returned in JSON response
        public const string STATUS_STOPPED = "stopped";
        public const string STATUS_PAUSED = "paused";
        public const string STATUS_RUNNING = "running";

        public const string CHARACTERS_ALLOWED_ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public const string CHARACTERS_ALLOWED_UPPER_ONLY = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string CHARACTERS_ALLOWED_LOWER_ONLY = "abcdefghijklmnopqrstuvwxyz";
        public const string CHARACTERS_ALLOWED_UPPER_NUMERIC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public const string CHARACTERS_ALLOWED_LOWER_NUMERIC = "abcdefghijklmnopqrstuvwxyz0123456789";
        public const string CHARACTERS_ALLOWED_ALPHA_NUMERIC = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    }
}
