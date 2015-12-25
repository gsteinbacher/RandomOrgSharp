using System;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp
{
    internal static class JsonHelper
    {
        internal static string JsonToString(JToken token, string defaultValue = null)
        {
            if (token == null)
                return defaultValue;

            return token.ToString();
        }

        internal static int JsonToInt(JToken token, int defaultValue = 0)
        {
            if (token == null)
                return defaultValue;

            int returnValue;
            return !int.TryParse(token.ToString(), out returnValue) ? defaultValue : returnValue;
        }

        internal static DateTime JsonToDateTime(JToken token, DateTime? defaultValue = null)
        {
            DateTime localDefaultValue = defaultValue ?? DateTime.MinValue;

            if (token == null)
                return localDefaultValue;

            DateTime returnValue;
            return !DateTime.TryParse(token.ToString(), out returnValue) ? localDefaultValue : returnValue;
        }

        internal static bool JsonToBoolean(JToken token, bool defaultValue = false)
        {
            if (token == null)
                return defaultValue;

            bool returnValue;
            return !bool.TryParse(token.ToString(), out returnValue) ? defaultValue : returnValue;
        }
    }
}
