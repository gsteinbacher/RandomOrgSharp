using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp
{
    public static class JsonHelper
    {
        public static string JsonToString(JToken token, string defaultValue = null)
        {
            if (token == null)
                return defaultValue;

            return token.ToString();
        }

        public static int JsonToInt(JToken token, int defaultValue = 0)
        {
            if (token == null)
                return defaultValue;

            int returnValue;
            return !int.TryParse(token.ToString(), out returnValue) ? defaultValue : returnValue;
        }

        public static DateTime JsonToDateTime(JToken token, DateTime? defaultValue = null)
        {
            DateTime localDefaultValue = defaultValue ?? DateTime.MinValue;

            if (token == null)
                return localDefaultValue;

            DateTime returnValue;
            return !DateTime.TryParse(token.ToString(), out returnValue) ? localDefaultValue : returnValue;
        }
    }
}
