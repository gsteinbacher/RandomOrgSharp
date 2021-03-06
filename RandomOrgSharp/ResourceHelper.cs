﻿using Obacher.RandomOrgSharp.Core.Properties;

namespace Obacher.RandomOrgSharp.Core
{
    public static class ResourceHelper
    {
        public static string GetString(string key)
        {
            return Strings.ResourceManager.GetString(key);
        }

        public static string GetString(string key, params object[] values)
        {
            var message = Strings.ResourceManager.GetString(key);

            if (!string.IsNullOrWhiteSpace(message) && values != null && values.Length > 0)
                return string.Format(message, values);

            return message;
        }
    }
}
