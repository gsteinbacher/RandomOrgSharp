using System;
using System.Collections.Specialized;
using System.Configuration;
using Obacher.Framework.Common.SystemWrapper.Interface;

namespace Obacher.Framework.Common.SystemWrapper
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Configuration.ConfigurationManager"/> class.
    /// </summary>
    public class ConfigurationManagerWrap : IConfigurationManager
    {
        /// <inheritdoc />
        public T GetAppSettingValue<T>(string key)
        {
            return GetAppSettingValue(key, default(T));
        }

        /// <inheritdoc />
        public T GetAppSettingValue<T>(string key, T defaultValue)
        {
            T value = defaultValue;

            object valueObject = ConfigurationManager.AppSettings[key];
            if (valueObject != null)
            {
                try
                {
                    value = (T)Convert.ChangeType(valueObject, typeof(T));
                }
                catch
                {
                    value = defaultValue;
                }
            }

            return value;

        }

        /// <inheritdoc />
        public NameValueCollection AppSettings => ConfigurationManager.AppSettings;

        /// <inheritdoc />
        public ConnectionStringSettingsCollection ConnectionStrings => ConfigurationManager.ConnectionStrings;

        /// <inheritdoc />
        public object GetSection(string pSectionName)
        {
            return ConfigurationManager.GetSection(pSectionName);
        }

        /// <inheritdoc />
        public Configuration OpenExeConfiguration(string pExePath)
        {
            return ConfigurationManager.OpenExeConfiguration(pExePath);
        }

        /// <inheritdoc />
        public Configuration OpenExeConfiguration(ConfigurationUserLevel pConfigurationUserLevel)
        {
            return ConfigurationManager.OpenExeConfiguration(pConfigurationUserLevel);
        }

        /// <inheritdoc />
        public Configuration OpenMachineConfiguration()
        {
            return ConfigurationManager.OpenMachineConfiguration();
        }

        /// <inheritdoc />
        public Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap pExeConfigurationFileMap, ConfigurationUserLevel pConfigurationUserLevel)
        {
            return ConfigurationManager.OpenMappedExeConfiguration(pExeConfigurationFileMap, pConfigurationUserLevel);
        }

        /// <inheritdoc />
        public Configuration OpenMappedMachineConfiguration(ConfigurationFileMap pConfigurationFileMap)
        {
            return ConfigurationManager.OpenMappedMachineConfiguration(pConfigurationFileMap);
        }

        /// <inheritdoc />
        public void RefreshSection(string pSectionName)
        {
            ConfigurationManager.RefreshSection(pSectionName);
        }
    }
}
