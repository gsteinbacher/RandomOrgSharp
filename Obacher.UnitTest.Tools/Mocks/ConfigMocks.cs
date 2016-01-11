using Moq;
using Obacher.RandomOrgSharp.Core;

namespace Obacher.UnitTest.Tools.Mocks
{
    public static class ConfigMocks
    {
        public const string MOCK_API_KEY = "mockApiKey";

        public static Mock<ISettingsManager> SetupApiKeyMock(string apiKey = null, Mock<ISettingsManager> settingsManagerMock = null)
        {
            if (settingsManagerMock == null)
                settingsManagerMock = new Mock<ISettingsManager>();

            settingsManagerMock.Setup(m => m.GetConfigurationValue<string>("apiKey"))
                .Returns(apiKey ?? MOCK_API_KEY);

            return settingsManagerMock;
        }
    }
}
