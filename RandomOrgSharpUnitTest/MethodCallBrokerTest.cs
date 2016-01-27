using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Obacher.RandomOrgSharp.Core.Response;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest
{
    [TestClass]
    public class MethodCallBrokerTest
    {
        [TestMethod]
        public void Generate_WhenCalledWithNoResponseHandlers_ShouldReturnTrue()
        {
            // Arrange
            const string request = "SomeRequest";

            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestBuilder> requestBuilderMock = new Mock<IRequestBuilder>();
            requestBuilderMock.Setup(m => m.Build(parameters.Object)).Returns(request);

            Mock<IRandomService> randomServiceMock = new Mock<IRandomService>();
            randomServiceMock.Setup(m => m.SendRequest(request));

            // Act
            MethodCallBroker target = new MethodCallBroker(requestBuilderMock.Object, randomServiceMock.Object);
            var actual = target.Generate(parameters.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public async Task GenerateAsync_WhenCalledWithNoResponseHandlers_ShouldReturnTrue()
        {
            // Arrange
            const string request = "SomeRequest";

            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestBuilder> requestBuilderMock = new Mock<IRequestBuilder>();
            requestBuilderMock.Setup(m => m.Build(parameters.Object)).Returns(request);

            Mock<IRandomService> randomServiceMock = new Mock<IRandomService>();
            randomServiceMock.Setup(m => m.SendRequestAsync(request)).ReturnsAsync(string.Empty);

            // Act
            MethodCallBroker target = new MethodCallBroker(requestBuilderMock.Object, randomServiceMock.Object);
            var actual = await target.GenerateAsync(parameters.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void Generate_WhenCalledWithResponseHandlerThatReturnsTrue_ShouldReturnTrue()
        {
            // Arrange
            const string request = "SomeRequest";

            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestBuilder> requestBuilderMock = new Mock<IRequestBuilder>();
            requestBuilderMock.Setup(m => m.Build(parameters.Object)).Returns(request);

            Mock<IRandomService> randomServiceMock = new Mock<IRandomService>();
            randomServiceMock.Setup(m => m.SendRequest(request));

            Mock<IResponseHandlerFactory> responseHandlerMock = new Mock<IResponseHandlerFactory>();
            responseHandlerMock.Setup(m => m.Execute(parameters.Object, It.IsAny<string>())).Returns(true);

            // Act
            MethodCallBroker target = new MethodCallBroker(requestBuilderMock.Object, randomServiceMock.Object);
            var actual = target.Generate(parameters.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public async Task GenerateAsync_WhenCalledWithResponseHandlerThatReturnsTrue_ShouldReturnTrue()
        {
            // Arrange
            const string request = "SomeRequest";

            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestBuilder> requestBuilderMock = new Mock<IRequestBuilder>();
            requestBuilderMock.Setup(m => m.Build(parameters.Object)).Returns(request);

            Mock<IRandomService> randomServiceMock = new Mock<IRandomService>();
            randomServiceMock.Setup(m => m.SendRequestAsync(request)).ReturnsAsync(string.Empty);

            Mock<IResponseHandlerFactory> responseHandlerMock = new Mock<IResponseHandlerFactory>();
            responseHandlerMock.Setup(m => m.Execute(parameters.Object, It.IsAny<string>())).Returns(true);

            // Act
            MethodCallBroker target = new MethodCallBroker(requestBuilderMock.Object, randomServiceMock.Object);
            var actual = await target.GenerateAsync(parameters.Object);

            // Assert
            actual.Should().Be.True();
        }


        [TestMethod]
        public void Generate_WhenCalledWithResponseHandlerThatReturnsFalse_ShouldReturnFalse()
        {
            // Arrange
            const string request = "SomeRequest";

            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestBuilder> requestBuilderMock = new Mock<IRequestBuilder>();
            requestBuilderMock.Setup(m => m.Build(parameters.Object)).Returns(request);

            Mock<IRandomService> randomServiceMock = new Mock<IRandomService>();
            randomServiceMock.Setup(m => m.SendRequest(request));

            Mock< IPrecedingRequestCommandFactory> requestCommandMock = new Mock<IPrecedingRequestCommandFactory>();
            requestCommandMock.Setup(m => m.Execute(parameters.Object));

            Mock<IResponseHandler> responseHandlerMock = new Mock<IResponseHandler>();
            responseHandlerMock.Setup(m => m.CanHandle(parameters.Object)).Returns(true);
            responseHandlerMock.Setup(m => m.Handle(parameters.Object, It.IsAny<string>())).Returns(false);

            ResponseHandlerFactory responseHandlerFactory = new ResponseHandlerFactory(responseHandlerMock.Object);

            // Act
            MethodCallBroker target = new MethodCallBroker(requestBuilderMock.Object, randomServiceMock.Object, requestCommandMock.Object, responseHandlerFactory);
            var actual = target.Generate(parameters.Object);

            // Assert
            actual.Should().Be.False();
        }

        [TestMethod]
        public async Task GenerateAsync_WhenCalledWithResponseHandlerThatReturnsFalse_ShouldReturnFalse()
        {

            // Arrange
            const string request = "SomeRequest";

            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestBuilder> requestBuilderMock = new Mock<IRequestBuilder>();
            requestBuilderMock.Setup(m => m.Build(parameters.Object)).Returns(request);

            Mock<IRandomService> randomServiceMock = new Mock<IRandomService>();
            randomServiceMock.Setup(m => m.SendRequest(request));

            Mock<IPrecedingRequestCommandFactory> requestCommandMock = new Mock<IPrecedingRequestCommandFactory>();
            requestCommandMock.Setup(m => m.Execute(parameters.Object));

            Mock<IResponseHandler> responseHandlerMock = new Mock<IResponseHandler>();
            responseHandlerMock.Setup(m => m.CanHandle(parameters.Object)).Returns(true);
            responseHandlerMock.Setup(m => m.Handle(parameters.Object, It.IsAny<string>())).Returns(false);

            ResponseHandlerFactory responseHandlerFactory = new ResponseHandlerFactory(responseHandlerMock.Object);

            // Act
            MethodCallBroker target = new MethodCallBroker(requestBuilderMock.Object, randomServiceMock.Object, requestCommandMock.Object, responseHandlerFactory);
            var actual = await target.GenerateAsync(parameters.Object);

            // Assert
            actual.Should().Be.False();
        }
    }
}


