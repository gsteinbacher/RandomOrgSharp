namespace Obacher.RandomOrgSharp.Core
{
    public static class RandomOrgConstants
    {
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
