using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Service;
using Obacher.UnitTest.Tools;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest
{
    [TestClass]
    public class RandomOrgApiServiceTest
    {
        [TestMethod]
        public void SendRequest_ShouldCreateRequestAndRespondWithStream()
        {
            // arrange
            const string requestString = "Test Request";
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestString);

            const string expected = "response content";
            byte[] expectedBytes = Encoding.UTF8.GetBytes(expected);

            using (var requestStream = new MemoryStream())
            using (var responseStream = new MemoryStream())
            {
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Position = 0;

                responseStream.Write(expectedBytes, 0, expectedBytes.Length);
                responseStream.Position = 0;

                var responseMock = new Mock<IHttpWebResponse>();
                responseMock.Setup(c => c.GetResponseStream()).Returns(responseStream);

                var requestMock = new Mock<IHttpWebRequest>();
                requestMock.Setup(c => c.GetRequestStream()).Returns(requestStream);
                requestMock.Setup(c => c.GetResponse()).Returns(responseMock.Object);

                var webRequestFactoryMock = new Mock<IHttpWebRequestFactory>();
                webRequestFactoryMock.Setup(c => c.Create(It.IsAny<string>()))
                    .Returns(requestMock.Object);

                var settingsManagerMock = MockHelper.MockSettingsManager();

                // Act
                RandomOrgApiService target = new RandomOrgApiService(settingsManagerMock.Object,
                    webRequestFactoryMock.Object);
                var actual = target.SendRequest(requestString);

                // assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod]
        public async Task SendRequestAsync_ShouldCreateRequestAndRespondWithStream()
        {
            // arrange
            const string requestString = "Test Request";
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestString);

            const string expected = "response content";
            byte[] expectedBytes = Encoding.UTF8.GetBytes(expected);

            using (var requestStream = new MemoryStream())
            using (var responseStream = new MemoryStream())
            {
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Position = 0;

                responseStream.Write(expectedBytes, 0, expectedBytes.Length);
                responseStream.Position = 0;

                var responseMock = new Mock<IHttpWebResponse>();
                responseMock.Setup(c => c.GetResponseStream()).Returns(responseStream);

                var requestMock = new Mock<IHttpWebRequest>();
                requestMock.Setup(c => c.GetRequestStream()).Returns(requestStream);
                requestMock.Setup(c => c.GetResponseAsync()).ReturnsAsync(responseMock.Object);

                var webRequestFactoryMock = new Mock<IHttpWebRequestFactory>();
                webRequestFactoryMock.Setup(c => c.Create(It.IsAny<string>()))
                    .Returns(requestMock.Object);

                var settingsManagerMock = MockHelper.MockSettingsManager();

                // Act
                RandomOrgApiService target = new RandomOrgApiService(settingsManagerMock.Object,
                    webRequestFactoryMock.Object);
                var actual = await target.SendRequestAsync(requestString);

                // assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "Operation is not valid")]
        public void SendRequest_ShouldThrowException()
        {
            // arrange
            const string requestString = "Test Request";

            var requestMock = new Mock<IHttpWebRequest>();
            requestMock.Setup(c => c.GetRequestStream()).Throws(new ProtocolViolationException());

            var webRequestFactoryMock = new Mock<IHttpWebRequestFactory>();
            webRequestFactoryMock.Setup(c => c.Create(It.IsAny<string>()))
                .Returns(requestMock.Object);

            var settingsManagerMock = MockHelper.MockSettingsManager();

            // Act
            RandomOrgApiService target = new RandomOrgApiService(settingsManagerMock.Object,
                webRequestFactoryMock.Object);
            target.SendRequest(requestString);
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "Operation is not valid")]
        public async Task SendRequestAsync_ShouldThrowException()
        {
            // arrange
            const string requestString = "Test Request";

            var requestMock = new Mock<IHttpWebRequest>();
            requestMock.Setup(c => c.GetRequestStream()).Throws(new ProtocolViolationException());

            var webRequestFactoryMock = new Mock<IHttpWebRequestFactory>();
            webRequestFactoryMock.Setup(c => c.Create(It.IsAny<string>()))
                .Returns(requestMock.Object);

            var settingsManagerMock = MockHelper.MockSettingsManager();

            // Act
            RandomOrgApiService target = new RandomOrgApiService(settingsManagerMock.Object,
                webRequestFactoryMock.Object);
            await target.SendRequestAsync(requestString);
        }

    }
}
