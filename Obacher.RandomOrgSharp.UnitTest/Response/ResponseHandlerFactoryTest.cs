using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Response
{
    [TestClass]
    public class ResponseHandlerFactoryTest
    {
        [TestMethod]
        public void Execute_WhenCalledWithNoParameters_ShouldReturnTrue()
        {
            // Arrange
            const string response = "Test Response";
            Mock<IParameters> parameters = new Mock<IParameters>();

            // Act
            ResponseHandlerFactory target = new ResponseHandlerFactory();
            var actual = target.Execute(parameters.Object, response);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void Execute_WhenCalledWithResponseHandlerThatReturnsTrue_ShouldReturnTrue()
        {
            // Arrange
            const string response = "Test Response";
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IResponseHandler> responseHandlerMock = new Mock<IResponseHandler>();
            responseHandlerMock.Setup(m => m.Handle(parameters.Object, response)).Returns(true);

            // Act
            ResponseHandlerFactory target = new ResponseHandlerFactory(responseHandlerMock.Object);
            var actual = target.Execute(parameters.Object, response);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void Execute_WhenCalledWithResponseHandlerThatReturnsFalse_ShouldReturnFalse()
        {
            // Arrange
            const string response = "Test Response";
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IResponseHandler> responseHandlerMock = new Mock<IResponseHandler>();
            responseHandlerMock.Setup(m => m.Handle(parameters.Object, response)).Returns(false);

            // Act
            ResponseHandlerFactory target = new ResponseHandlerFactory(responseHandlerMock.Object);
            var actual = target.Execute(parameters.Object, response);

            // Assert
            actual.Should().Be.False();
        }

        [TestMethod]
        public void GetHandler_WhenCalled_ShouldReturnHandler()
        {
            // Arrange
            Mock<IResponseHandler> responseHandlerMock = new Mock<IResponseHandler>();

            // Act
            ResponseHandlerFactory target = new ResponseHandlerFactory(responseHandlerMock.Object);
            var actual = target.GetHandler(responseHandlerMock.Object.GetType());

            // Assert
            actual.Should().Equal(responseHandlerMock.Object);
        }


        [TestMethod]
        public void GetHandler_WhenNoTypeFound_ShouldReturnNull()
        {
            // Arrange
            Mock<IResponseHandler> responseHandlerMock = new Mock<IResponseHandler>();

            // Act
            ResponseHandlerFactory target = new ResponseHandlerFactory(responseHandlerMock.Object);
            var actual = target.GetHandler(typeof(Mock));

            // Assert
            actual.Should().Be.Null();
        }
    }
}

