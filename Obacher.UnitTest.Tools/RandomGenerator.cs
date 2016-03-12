using System;

namespace Obacher.UnitTest.Tools
{
    public static class RandomGenerator
    {
        static readonly Random _random = new Random();

        public static int GetInteger(int minValue = int.MinValue, int maxMalue = int.MaxValue)
        {
            return _random.Next(minValue, maxMalue);
        }

        public static DateTime GetDateTime()
        {
            return GetDateTime(DateTime.MinValue, DateTime.MaxValue);
        }

        public static DateTime GetDateTime(DateTime minValue)
        {
            return GetDateTime(minValue, DateTime.MaxValue);
        }

        public static DateTime GetDateTime(DateTime minValue, DateTime maxValue)
        {
            return GetDate(minValue, maxValue).AddSeconds(_random.Next(0, 863399));
        }

        public static DateTime GetDate()
        {
            return GetDate(DateTime.MinValue, DateTime.MaxValue);
        }

        public static DateTime GetDate(DateTime minValue)
        {
            return GetDate(minValue, DateTime.MaxValue);
        }

        public static DateTime GetDate(DateTime minValue, DateTime maxValue)
        {
            int range = maxValue.Date.Subtract(minValue.Date).Days;
            return minValue.AddDays(_random.Next(0, range));
        }
    }
}
