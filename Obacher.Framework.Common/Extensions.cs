namespace Obacher.Framework.Common
{
    public static class Extensions
    {
        public static bool Between(this int value, int startRange, int endRange)
        {
            return value >= startRange && value <= endRange;
        }
    }
}
