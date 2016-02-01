namespace Obacher.RandomOrgSharp.Core
{
    public interface ISettingsManager
    {
        string GetApiKey();
        string GetUrl();
        int GetHttpRequestTimeout();
        int GetHttpReadWriteTimeout();
    }
}