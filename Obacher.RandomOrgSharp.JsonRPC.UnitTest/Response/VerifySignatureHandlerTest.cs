using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Response;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Response
{
    [TestClass]
    public class VerifySignatureHandlerTest
    {
        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "do not match")]
        public void Handle_WhenResultNodeNotFoundInJson_ShouldThrowException()
        {
            // Arrange
            string verifiedResponse = string.Empty;

            Mock<IRandomService> serviceMock = new Mock<IRandomService>();
            serviceMock.Setup(m => m.SendRequest(It.IsAny<string>())).Returns(verifiedResponse);

            var input = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("notResultNode")
                );

            // Act
            VerifySignatureHandler target = new VerifySignatureHandler();
            target.Handle(null, input.ToString());

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "do not match")]
        public void Handle_WhenRandomNodeNotFoundInJson_ShouldThrowException()
        {
            // Arrange
            string verifiedResponse = string.Empty;
            Mock<IRandomService> serviceMock = new Mock<IRandomService>();
            serviceMock.Setup(m => m.SendRequest(It.IsAny<string>())).Returns(verifiedResponse);

            var input = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("result",
                    new JObject(
                        new JProperty("notrandom", new JObject()))
                    )
                );

            // Act
            VerifySignatureHandler target = new VerifySignatureHandler();
            target.Handle(null, input.ToString());

            // Assert
        }


        [TestMethod]
        public void Handle_WhenAuthenticityIsTrue_ShouldReturnTrue()
        {
            // Arrange
            var verifiedResponse = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("result",
                    new JObject(
                        new JProperty("authenticity", true))
                    ),
                new JProperty("id", 1234)
                );

            Mock<IRandomService> serviceMock = new Mock<IRandomService>();
            serviceMock.Setup(m => m.SendRequest(It.IsAny<string>())).Returns(verifiedResponse.ToString);

            var input = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("result",
                    new JObject(
                        new JProperty("random", new JObject()))
                    )
                );

            // Act
            VerifySignatureHandler target = new VerifySignatureHandler(serviceMock.Object);
            var actual = target.Handle(null, input.ToString());

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "do not match")]
        public void Handle_WhenAuthenticityIsFalse_ShouldThrowException()
        {
            // Arrange
            var verifiedResponse = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("result",
                    new JObject(
                        new JProperty("authenticity", false))
                    ),
                new JProperty("id", 1234)
                );


            Mock<IRandomService> serviceMock = new Mock<IRandomService>();
            serviceMock.Setup(m => m.SendRequest(It.IsAny<string>())).Returns(verifiedResponse.ToString);

            var input = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("result",
                    new JObject(
                        new JProperty("random", new JObject()))
                    )
                );

            // Act
            VerifySignatureHandler target = new VerifySignatureHandler();
            target.Handle(null, input.ToString());

            // Assert
        }

        [TestMethod]
        public void Process_WhenCalled_ShouldSetVerifyOriginatorToTrueAndReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.SetupProperty(p => p.VerifyOriginator, false);

            // Act
            VerifySignatureHandler target = new VerifySignatureHandler();
            var actual = target.Process(parametersMock.Object);

            // Assert
            parametersMock.Object.VerifyOriginator.Should().Be.True();
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenVerifyOriginatorIsFalse_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(p => p.VerifyOriginator).Returns(false);

            // Act
            VerifySignatureHandler target = new VerifySignatureHandler();
            var actual = target.CanHandle(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }

        [TestMethod]
        public void CanHandle_WhenVerifyOriginatorIsTrue_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(p => p.VerifyOriginator).Returns(true);

            // Act
            VerifySignatureHandler target = new VerifySignatureHandler();
            var actual = target.CanHandle(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }


        [TestMethod]
        public void CanProcess_WhenCalled_ShouldAlwaysReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(p => p.VerifyOriginator).Returns(true);

            // Act
            VerifySignatureHandler target = new VerifySignatureHandler();
            var actual = target.CanProcess(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }
    }
}