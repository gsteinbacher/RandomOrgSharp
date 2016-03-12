using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Response;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Response
{
    [TestClass]
    public class ErrorHandlerTest
    {
        [TestMethod]
        public void CanHandle_WhenCalled_ShouldReturnTrue()
        {
            // Arrange

            // Act
            ErrorHandler target = new ErrorHandler(null);
            var actual = target.CanHandle(null);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void Handle_WhenErrorInfoNull_ShouldHaveErrors()
        {
            // Arrange
            Mock<IResponseParser> responseParserMock = new Mock<IResponseParser>();
            responseParserMock.Setup(m => m.Parse(It.IsAny<string>())).Returns((ErrorResponseInfo)null);
            responseParserMock.Setup(m => m.CanParse(It.IsAny<IParameters>())).Returns(true);

            // Act
            ErrorHandler target = new ErrorHandler(responseParserMock.Object);
            var actual = target.Handle(null, string.Empty);

            // Assert
            target.HasError().Should().Be.False();
            actual.Should().Be.True();                  
            target.ErrorInfo.Should().Be.Null();
        }

        [TestMethod]
        public void Handle_WhenErrorInfoEmpty_ShouldNotHaveErrors()
        {
            // Arrange
            Mock<IResponseParser> responseParserMock = new Mock<IResponseParser>();
            responseParserMock.Setup(m => m.Parse(It.IsAny<string>())).Returns(ErrorResponseInfo.Empty());
            responseParserMock.Setup(m => m.CanParse(It.IsAny<IParameters>())).Returns(true);

            // Act
            ErrorHandler target = new ErrorHandler(responseParserMock.Object);
            var actual = target.Handle(null, string.Empty);

            // Assert
            target.HasError().Should().Be.False();
            target.ErrorInfo.Should().Not.Be.Null();
            actual.Should().Be.True();          // Should be no problems with running the processing process
            responseParserMock.Setup(m => m.CanParse(It.IsAny<IParameters>())).Returns(true);
        }

        [TestMethod]
        public void Handle_WhenErrorInfoCodeIsNotZero_ShouldHaveError()
        {
            // Arrange
            const int expected = 110;
            Mock<IResponseParser> responseParserMock = new Mock<IResponseParser>();
            responseParserMock.Setup(m => m.Parse(It.IsAny<string>())).Returns(new ErrorResponseInfo(string.Empty, 0, expected, string.Empty));
            responseParserMock.Setup(m => m.CanParse(It.IsAny<IParameters>())).Returns(true);

            // Act
            ErrorHandler target = new ErrorHandler(responseParserMock.Object);
            var actual = target.Handle(null, string.Empty);

            // Assert
            target.HasError().Should().Be.True();
            actual.Should().Be.True();
            target.ErrorInfo.Should().Not.Be.Null();
            target.ErrorInfo.Code.Should().Equal(expected);
        }

        [TestMethod]
        public void Handle_WhenCanParseReturnsFalse_ShouldNotHaveError()
        {
            // Arrange
            Mock<IResponseParser> responseParserMock = new Mock<IResponseParser>();
            responseParserMock.Setup(m => m.CanParse(It.IsAny<IParameters>())).Returns(false);

            // Act
            ErrorHandler target = new ErrorHandler(responseParserMock.Object);
            var actual = target.Handle(null, string.Empty);

            // Assert
            responseParserMock.Verify(m => m.Parse(It.IsAny<string>()), Times.Never);
            target.HasError().Should().Be.False();
            actual.Should().Be.True();          // Should be no problems with running the processing process
        }
    }
}
