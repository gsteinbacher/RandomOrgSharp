using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Response;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Response
{
    [TestClass]
    public class JsonResponseParserFactoryTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Execute_WhenCalledWithNoParameters_ShouldThrowException()
        {
            // Arrange
            const string response = "Test Response";

            // Act
            JsonResponseParserFactory target = new JsonResponseParserFactory();
            target.Handle(null, response);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "Method not")]
        public void Execute_WhenCanParseReturnsFalse_ShouldThrowException()
        {
            // Arrange
            const string response = "Test Response";
            Mock<IParameters> parameters = new Mock<IParameters>();

            Mock<IResponseParser> responseHandlerMock = new Mock<IResponseParser>();
            responseHandlerMock.Setup(m => m.CanParse(parameters.Object)).Returns(false);

            // Act
            JsonResponseParserFactory target = new JsonResponseParserFactory(responseHandlerMock.Object);
            target.Handle(parameters.Object, response);

            // Assert
        }

        [TestMethod]
        public void Execute_WhenParseCalled_ShouldReturnTrue()
        {
            // Arrange
            const string response = "Test Response";
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IResponseInfo> responseInfoMock = new Mock<IResponseInfo>();

            Mock<IResponseParser> responseHandlerMock = new Mock<IResponseParser>();
            responseHandlerMock.Setup(m => m.CanParse(parameters.Object)).Returns(true);
            responseHandlerMock.Setup(m => m.Parse(response)).Returns(responseInfoMock.Object);

            // Act
            JsonResponseParserFactory target = new JsonResponseParserFactory(responseHandlerMock.Object);
            var actual = target.Handle(parameters.Object, response);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void Execute_WhenParseCalled_ShouldSetResponseInfoToMock()
        {
            // Arrange
            const string response = "Test Response";
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IResponseInfo> responseInfoMock = new Mock<IResponseInfo>();

            Mock<IResponseParser> responseHandlerMock = new Mock<IResponseParser>();
            responseHandlerMock.Setup(m => m.CanParse(parameters.Object)).Returns(true);
            responseHandlerMock.Setup(m => m.Parse(response)).Returns(responseInfoMock.Object);

            // Act
            JsonResponseParserFactory target = new JsonResponseParserFactory(responseHandlerMock.Object);
            target.Handle(parameters.Object, response);

            // Assert
            target.ResponseInfo.Should().Equal(responseInfoMock.Object);
        }

        [TestMethod]
        public void GetHandler_WhenCalled_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IResponseParser> responseHandlerMock = new Mock<IResponseParser>();

            // Act
            JsonResponseParserFactory target = new JsonResponseParserFactory(responseHandlerMock.Object);
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Be.True();
        }
    }
}

