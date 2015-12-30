using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Method;
using Obacher.RandomOrgSharp.Error;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.BasicMethod
{
    [TestClass]
    public class BasicMethodTest
    {
        [TestMethod]
        public void WhenGenerateCalled_ExpectWorkflowToMatch()
        {
            // Arrange
            const string apiKey = "API_KEY";
            const int advisoryDelay = 4444;
            const int id = 5555;

            var mockJsonRequest = new Mock<JObject>();
            var mockJsonResponse = new Mock<JObject>();

            var mockParameters = new Mock<IParameters>();
            mockParameters.Setup(p => p.ApiKey).Returns(apiKey);
            mockParameters.Setup(p => p.Id).Returns(id);
            mockParameters.Setup(p => p.MethodType).Returns(MethodType.Integer);
            mockParameters.Setup(p => p.VerifyOriginator).Returns(false);

            var expected = new DataResponse<int>(null, Enumerable.Empty<int>(), DateTime.Now, 0, 0, 0, advisoryDelay, 0);

            var mockCallManager = new Mock<IAdvisoryDelayManager>();
            mockCallManager.Setup(m => m.Delay());
            mockCallManager.Setup(m => m.SetAdvisoryDelay(advisoryDelay));

            var mockRequestBuilder = new Mock<IJsonRequestBuilder>();
            mockRequestBuilder.Setup(m => m.Create(mockParameters.Object)).Returns(mockJsonRequest.Object);

            var mockResponseHandlerFactory = new Mock<IResponseHandlerFactory>();
            mockResponseHandlerFactory.Setup(m => m.Execute(mockJsonResponse.Object, mockParameters.Object));
            mockResponseHandlerFactory.Setup(m => m.GetHandler(It.IsAny<Type>()));

            var mockService = new Mock<IRandomService>();
            mockService.Setup(m => m.SendRequest(mockJsonRequest.Object)).Returns(mockJsonResponse.Object);

            var mockResponseParser = new Mock<IParser>();
            mockResponseParser.Setup(m => m.Parse(mockJsonResponse.Object)).Returns(expected);
            var mockResponseParserFactory = new Mock<IJsonResponseParserFactory>();
            mockResponseParserFactory.Setup(m => m.GetParser(mockParameters.Object)).Returns(mockResponseParser.Object);

            // Act
            var target = new DataMethodManager<int>(mockService.Object, mockRequestBuilder.Object, mockResponseHandlerFactory.Object);
            var actual = target.Generate(mockParameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public async Task WhenExecuteAsyncCalled_ExpectListOfNumbersReturned()
        {
            // Arrange
            const string apiKey = "API_KEY";
            const int advisoryDelay = 4444;
            const int id = 5555;

            var mockJsonRequest = new Mock<JObject>();
            var mockJsonResponse = new Mock<JObject>();

            var mockParameters = new Mock<IParameters>();
            mockParameters.Setup(p => p.ApiKey).Returns(apiKey);
            mockParameters.Setup(p => p.Id).Returns(id);
            mockParameters.Setup(p => p.MethodType).Returns(MethodType.Integer);
            mockParameters.Setup(p => p.VerifyOriginator).Returns(false);

            var expected = new DataResponse<int>(null, Enumerable.Empty<int>(), DateTime.Now, 0, 0, 0, advisoryDelay, 0);

            var mockCallManager = new Mock<IAdvisoryDelayManager>();
            mockCallManager.Setup(m => m.Delay());
            mockCallManager.Setup(m => m.SetAdvisoryDelay(advisoryDelay));

            var mockRequestBuilder = new Mock<IJsonRequestBuilder>();
            mockRequestBuilder.Setup(m => m.Create(mockParameters.Object)).Returns(mockJsonRequest.Object);

            var mockService = new Mock<IRandomService>();
            mockService.Setup(m => m.SendRequestAsync(mockJsonRequest.Object)).ReturnsAsync(mockJsonResponse.Object);

            var mockResponseHandlerFactory = new Mock<IResponseHandlerFactory>();
            mockResponseHandlerFactory.Setup(m => m.Execute(mockJsonResponse.Object, mockParameters.Object));
            mockResponseHandlerFactory.Setup(m => m.GetHandler(It.IsAny<Type>()));

            var mockErrorHandler = new Mock<IErrorHandler>();
            mockErrorHandler.Setup(m => m.HasError(It.IsAny<JObject>())).Returns(false);

            var mockResponseParser = new Mock<IParser>();
            mockResponseParser.Setup(m => m.Parse(mockJsonResponse.Object)).Returns(expected);
            var mockResponseParserFactory = new Mock<IJsonResponseParserFactory>();
            mockResponseParserFactory.Setup(m => m.GetParser(mockParameters.Object)).Returns(mockResponseParser.Object);

            // Act
            var target = new DataMethodManager<int>(mockService.Object, mockRequestBuilder.Object, mockResponseHandlerFactory.Object);
            var actual = await target.GenerateAsync(mockParameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
