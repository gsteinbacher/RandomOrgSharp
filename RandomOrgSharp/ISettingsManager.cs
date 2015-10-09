namespace Obacher.RandomOrgSharp
{
    public interface ISettingsManager
    {
        T GetConfigurationValue<T>(string key);
        T GetConfigurationValue<T>(string key, T defaultValue);
    }
}