using System;
using System.Configuration;

namespace Obacher.RandomOrgSharp
{
    public class SettingsManager : ISettingsManager
    {
        private static ISettingsManager _settingsManager;
        public static ISettingsManager Instance
        {
            get { return _settingsManager ?? (_settingsManager = new SettingsManager()); }
            set { _settingsManager = value; }
        }

        /// <summary>
        /// Retrieve the value from the configuration file based on the specified key
        /// </summary>
        /// <typeparam name="T">Type of the value to be returend</typeparam>
        /// <param name="key">Name in the configuration file</param>
        /// <returns>Value in the configuration file.  Returns default(T) if the value is not found</returns>
        public T GetConfigurationValue<T>(string key)
        {
            return GetConfigurationValue<T>(key, default(T));
        }

        /// <summary>
        /// Retrieve the value from the configuration file based on the specified key
        /// </summary>
        /// <typeparam name="T">Type of the value to be returend</typeparam>
        /// <param name="key">Name in the configuration file</param>
        /// <param name="defaultValue">Default value if key is not found in configuration file</param>
        /// <returns>Value in the configuration file.  Returns <param name="defaultValue"></param> if the value is not found</returns>
        public T GetConfigurationValue<T>(string key, T defaultValue)
        {
            T value = defaultValue;

            object timeoutObject = ConfigurationManager.AppSettings[key];
            if (timeoutObject != null)
            {
                try
                {
                    value = (T)Convert.ChangeType(timeoutObject, typeof(T));
                }
                catch
                {
                    value = defaultValue;
                }
            }

            return value;

        }
    }
}
