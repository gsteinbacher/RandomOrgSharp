namespace Obacher.RandomOrgSharp.Core
{
    public interface ISettingsManager
    {
        T GetConfigurationValue<T>(string key);
        T GetConfigurationValue<T>(string key, T defaultValue);
    }
}