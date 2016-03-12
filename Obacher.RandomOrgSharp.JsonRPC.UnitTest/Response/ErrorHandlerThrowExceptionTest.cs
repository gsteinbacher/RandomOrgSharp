using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Response;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Response
{
    [TestClass]
    public class ErrorHandlerThrowExceptionTest
    {
        [TestMethod]
        public void CanHandle_WhenCalled_ShouldReturnTrue()
        {
            // Arrange

            // Act
            ErrorHandlerThrowException target = new ErrorHandlerThrowException(null);
            var actual = target.CanHandle(null);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void Handle_WhenErrorInfoNull_ShouldReturnFalse()
        {
            // Arrange
            Mock<IResponseParser> responseParserMock = new Mock<IResponseParser>();
            responseParserMock.Setup(m => m.Parse(It.IsAny<string>())).Returns((ErrorResponseInfo)null);
            responseParserMock.Setup(m => m.CanParse(It.IsAny<IParameters>())).Returns(true);

            // Act
            ErrorHandlerThrowException target = new ErrorHandlerThrowException(responseParserMock.Object);
            var actual = target.Handle(null, string.Empty);

            // Assert
            target.HasError().Should().Be.False();
            actual.Should().Be.True();              // Method call to parse error should run successfully
            target.ErrorInfo.Should().Be.Null();
        }

        [TestMethod]
        public void Handle_WhenErrorInfoEmpty_ShouldReturnFalse()
        {
            // Arrange
            Mock<IResponseParser> responseParserMock = new Mock<IResponseParser>();
            responseParserMock.Setup(m => m.Parse(It.IsAny<string>())).Returns(ErrorResponseInfo.Empty());
            responseParserMock.Setup(m => m.CanParse(It.IsAny<IParameters>())).Returns(true);

            // Act
            ErrorHandlerThrowException target = new ErrorHandlerThrowException(responseParserMock.Object);
            var actual = target.Handle(null, string.Empty);

            // Assert
            target.HasError().Should().Be.False();
            actual.Should().Be.True();              // Method call to parse error should run successfully
            target.ErrorInfo.Should().Not.Be.Null();
            responseParserMock.Setup(m => m.CanParse(It.IsAny<IParameters>())).Returns(true);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgException))]
        public void Handle_WhenErrorInfoCodeIsNotZero_ShouldThrowException()
        {
            // Arrange
            Mock<IResponseParser> responseParserMock = new Mock<IResponseParser>();
            responseParserMock.Setup(m => m.Parse(It.IsAny<string>())).Returns(new ErrorResponseInfo(string.Empty, 0, 101, string.Empty));
            responseParserMock.Setup(m => m.CanParse(It.IsAny<IParameters>())).Returns(true);

            // Act
            ErrorHandlerThrowException target = new ErrorHandlerThrowException(responseParserMock.Object);
            target.Handle(null, string.Empty);

            // Assert
        }

        [TestMethod]
        public void Handle_WhenCanParseReturnsFalse_ShouldNotHaveError()
        {
            // Arrange
            Mock<IResponseParser> responseParserMock = new Mock<IResponseParser>();
            responseParserMock.Setup(m => m.CanParse(It.IsAny<IParameters>())).Returns(false);

            // Act
            ErrorHandlerThrowException target = new ErrorHandlerThrowException(responseParserMock.Object);
            var actual = target.Handle(null, string.Empty);

            // Assert
            responseParserMock.Verify(m => m.Parse(It.IsAny<string>()), Times.Never);
            actual.Should().Be.True();              // Method call to parse error should run successfully
        }
    }
}
