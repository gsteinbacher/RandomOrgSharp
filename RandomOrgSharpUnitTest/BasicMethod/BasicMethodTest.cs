using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.BasicMethod;
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

            var mockResponse = new Mock<IBasicMethodResponse<int>>();
            mockResponse.Setup(m => m.AdvisoryDelay).Returns(advisoryDelay);

            var mockCallManager = new Mock<IMethodCallManager>();
            mockCallManager.Setup(m => m.CanSendRequest());
            mockCallManager.Setup(m => m.Delay());
            mockCallManager.Setup(m => m.ThrowExceptionOnError(mockJsonResponse.Object));
            mockCallManager.Setup(m => m.SetAdvisoryDelay(advisoryDelay));
            mockCallManager.Setup(m => m.VerifyResponse(mockParameters.Object, mockResponse.Object));

            var mockRequestBuilder = new Mock<IJsonRequestBuilder>();
            mockRequestBuilder.Setup(m => m.Create(mockParameters.Object)).Returns(mockJsonRequest.Object);

            var mockService = new Mock<IRandomOrgService>();
            mockService.Setup(m => m.SendRequest(mockJsonRequest.Object)).Returns(mockJsonResponse.Object);

            var mockResponseParser = new Mock<IParser>();
            mockResponseParser.Setup(m => m.Parse(mockJsonResponse.Object)).Returns(mockResponse.Object);
            var mockResponseParserFactory = new Mock<IJsonResponseParserFactory>();
            mockResponseParserFactory.Setup(m => m.GetParser(mockParameters.Object)).Returns(mockResponseParser.Object);

            // Act
            var target = new BasicMethod<int>(mockService.Object, mockCallManager.Object, mockRequestBuilder.Object, mockResponseParserFactory.Object);
            var actual = target.Generate(mockParameters.Object);

            // Assert
            actual.Should().Equal(mockResponse.Object);
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

            var mockResponse = new Mock<IBasicMethodResponse<int>>();
            mockResponse.Setup(m => m.AdvisoryDelay).Returns(advisoryDelay);

            var mockCallManager = new Mock<IMethodCallManager>();
            mockCallManager.Setup(m => m.CanSendRequest());
            mockCallManager.Setup(m => m.Delay());
            mockCallManager.Setup(m => m.ThrowExceptionOnError(mockJsonResponse.Object));
            mockCallManager.Setup(m => m.SetAdvisoryDelay(advisoryDelay));
            mockCallManager.Setup(m => m.VerifyResponse(mockParameters.Object, mockResponse.Object));

            var mockRequestBuilder = new Mock<IJsonRequestBuilder>();
            mockRequestBuilder.Setup(m => m.Create(mockParameters.Object)).Returns(mockJsonRequest.Object);

            var mockService = new Mock<IRandomOrgService>();
            mockService.Setup(m => m.SendRequestAsync(mockJsonRequest.Object)).ReturnsAsync(mockJsonResponse.Object);

            var mockResponseParser = new Mock<IParser>();
            mockResponseParser.Setup(m => m.Parse(mockJsonResponse.Object)).Returns(mockResponse.Object);
            var mockResponseParserFactory = new Mock<IJsonResponseParserFactory>();
            mockResponseParserFactory.Setup(m => m.GetParser(mockParameters.Object)).Returns(mockResponseParser.Object);

            // Act
            var target = new BasicMethod<int>(mockService.Object, mockCallManager.Object, mockRequestBuilder.Object, mockResponseParserFactory.Object);
            var actual = await target.GenerateAsync(mockParameters.Object);

            // Assert
            actual.Should().Equal(mockResponse.Object);

        }
    }
}
