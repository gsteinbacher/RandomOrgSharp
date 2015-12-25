using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Obacher.RandomOrgSharp;

namespace Obacher.UnitTest.Tools.Mocks
{
    public static class ConfigMocks
    {
        public const string MOCK_API_KEY = "mockApiKey";

        public static Mock<ISettingsManager> SetupApiKeyMock(string apiKey = null, Mock<ISettingsManager> settingsManagerMock = null)
        {
            if (settingsManagerMock == null)
                settingsManagerMock = new Mock<ISettingsManager>();

            settingsManagerMock.Setup(m => m.GetConfigurationValue<string>(RandomOrgConstants.APIKEY_KEY))
                .Returns(apiKey ?? MOCK_API_KEY);

            return settingsManagerMock;
        }
    }
}
