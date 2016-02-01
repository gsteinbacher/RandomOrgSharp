using Moq;
using Obacher.Framework.Common.SystemWrapper.Interface;
using Obacher.RandomOrgSharp.Core;

namespace Obacher.UnitTest.Tools.Mocks
{
    public static class MockHelper
    {
        const string MockApiKey = "mockApiKey";
        const string MockUrl = "http://mock.random.org";

        /// <summary>
        /// Mock the <see cref="RandomNumberGenerator"/> when generating an Id value that is stored in the request object
        /// </summary>
        /// <param name="id">Id to store, default is 0</param>
        /// <param name="randomNumberGeneratorMock">Random Number Generator mock object. a new instance is creeated if one is passed</param>
        /// <returns>The created mock object, or the same instance passed in if one is passed.</returns>
        public static Mock<IRandom> SetupIdMock(int id = 0, Mock<IRandom> randomNumberGeneratorMock = null)
        {
            if (randomNumberGeneratorMock == null)
                randomNumberGeneratorMock = new Mock<IRandom>();

            randomNumberGeneratorMock.Setup(m => m.Next())
                .Returns(id);

            return randomNumberGeneratorMock;
        }

        public static Mock<ISettingsManager> MockSettingsManager(string apiKey = null, string url = null, int requestTimeout = 18000, int readWriteTimeout = 18000, Mock<ISettingsManager> settingsManagerMock = null)
        {
            if (settingsManagerMock == null)
                settingsManagerMock = new Mock<ISettingsManager>();

            settingsManagerMock.Setup(m => m.GetApiKey()).Returns(apiKey ?? MockApiKey);
            settingsManagerMock.Setup(m => m.GetUrl()).Returns(url ?? MockUrl);
            settingsManagerMock.Setup(m => m.GetHttpRequestTimeout()).Returns(requestTimeout);
            settingsManagerMock.Setup(m => m.GetHttpReadWriteTimeout()).Returns(readWriteTimeout);

            return settingsManagerMock;
        }
    }
}