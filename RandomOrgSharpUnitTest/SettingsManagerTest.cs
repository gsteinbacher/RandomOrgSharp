using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.Framework.Common.SystemWrapper.Interface;
using Obacher.RandomOrgSharp.Core;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest
{
    [TestClass]
    public class SettingsManagerTest
    {
        [TestMethod]
        public void GetApiKey_WhenCalled_ExpectKeyReturned()
        {
            // Arrange
            const string expected = "APIKEY";
            Mock<IConfigurationManager> configManagerMock = new Mock<IConfigurationManager>();
            configManagerMock.Setup(m => m.GetAppSettingValue<string>(RandomOrgConstants.APIKEY_KEY)).Returns(expected);

            // Act
            SettingsManager target = new SettingsManager(configManagerMock.Object);
            var actual = target.GetApiKey();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRunTimeException), "API Key is not found")]
        public void GetApiKey_WhenNullValue_ExpectException()
        {
            // Arrange
            const string expected = null;
            Mock<IConfigurationManager> configManagerMock = new Mock<IConfigurationManager>();
            configManagerMock.Setup(m => m.GetAppSettingValue<string>(RandomOrgConstants.APIKEY_KEY)).Returns(expected);

            // Act
            SettingsManager target = new SettingsManager(configManagerMock.Object);
            target.GetApiKey();
        }

        [TestMethod]
        public void GetUrl_WhenCalled_ExpectValueReturned()
        {
            // Arrange
            const string expected = "APIKEY";
            Mock<IConfigurationManager> configManagerMock = new Mock<IConfigurationManager>();
            configManagerMock.Setup(m => m.GetAppSettingValue("Url", It.IsAny<string>())).Returns(expected);

            // Act
            SettingsManager target = new SettingsManager(configManagerMock.Object);
            var actual = target.GetUrl();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void GetHttpRequestTimeout_WhenCalled_ExpectValueReturned()
        {
            // Arrange
            const int expected = 12345;
            Mock<IConfigurationManager> configManagerMock = new Mock<IConfigurationManager>();
            configManagerMock.Setup(m => m.GetAppSettingValue("HttpRequestTimeout", It.IsAny<int>())).Returns(expected);

            // Act
            SettingsManager target = new SettingsManager(configManagerMock.Object);
            var actual = target.GetHttpRequestTimeout();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void GetHttpReadWriteTimeout_WhenCalled_ExpectValueReturned()
        {
            // Arrange
            const int expected = 12345;
            Mock<IConfigurationManager> configManagerMock = new Mock<IConfigurationManager>();
            configManagerMock.Setup(m => m.GetAppSettingValue("HttpReadWriteTimeout", It.IsAny<int>())).Returns(expected);

            // Act
            SettingsManager target = new SettingsManager(configManagerMock.Object);
            var actual = target.GetHttpReadWriteTimeout();

            // Assert
            actual.Should().Equal(expected);
        }
    }
}