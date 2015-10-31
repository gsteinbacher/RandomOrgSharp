using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.BasicMethod;
using Obacher.RandomOrgSharp.RequestParameters;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.BasicMethod
{
    [TestClass]
    public class BasicMethodTest
    {
        [TestMethod]
        public void WhenExecuteCalled_ExpectListOfNumbersReturned()
        {
            var expected = new List<int> { 1, 5, 4, 6, 6, 4 };
            var input = JObject.Parse(@"{
    jsonrpc: '2.0',
    result: {
                random: {
                    data: [" + String.Join(",", expected) + @"],
            completionTime: '2011-10-10 13:19:12Z'
        },
        bitsUsed: 16,
        bitsLeft: 199984,
        requestsLeft: 9999,
        advisoryDelay: 0
    },
    id: 42
}");
            var mockCallManager = new Mock<IMethodCallManager>();
            mockCallManager.Setup(m => m.CanSendRequest());
            mockCallManager.Setup(m => m.Delay());
            mockCallManager.Setup(m => m.ThrowExceptionOnError(It.IsAny<JObject>()));
            mockCallManager.Setup(m => m.SetAdvisoryDelay(It.IsAny<int>()));
            mockCallManager.Setup(m => m.VerifyResponse(It.IsAny<IRequestParameters>(), It.IsAny<BasicMethodResponse>()));

            var mockRequest = new Mock<IRequestParameters>();
            mockRequest.Setup(m => m.CreateJsonRequest()).Returns(It.IsAny<JObject>());

            var mockService = new Mock<IRandomOrgService>();
            mockService.Setup(m => m.SendRequest(It.IsAny<JObject>())).Returns(input);

            var target = new BasicMethod<int>(mockService.Object, mockCallManager.Object);
            var actual = target.Execute(mockRequest.Object);

            actual.Should().Equal(expected);
        }

        [TestMethod]
        public async Task WhenExecuteAsyncCalled_ExpectListOfNumbersReturned()
        {
            var expected = new List<int> {1, 5, 4, 6, 6, 4};
            var input = JObject.Parse(@"{
    jsonrpc: '2.0',
    result: {
                random: {
                    data: [" + String.Join(",", expected) + @"],
            completionTime: '2011-10-10 13:19:12Z'
        },
        bitsUsed: 16,
        bitsLeft: 199984,
        requestsLeft: 9999,
        advisoryDelay: 0
    },
    id: 42
}");

        var mockCallManager = new Mock<IMethodCallManager>();
            mockCallManager.Setup(m => m.CanSendRequest());
            mockCallManager.Setup(m => m.Delay());
            mockCallManager.Setup(m => m.ThrowExceptionOnError(It.IsAny<JObject>()));
            mockCallManager.Setup(m => m.SetAdvisoryDelay(It.IsAny<int>()));
            mockCallManager.Setup(m => m.VerifyResponse(It.IsAny<IRequestParameters>(), It.IsAny<BasicMethodResponse>()));

            var mockRequest = new Mock<IRequestParameters>();
            mockRequest.Setup(m => m.CreateJsonRequest()).Returns(It.IsAny<JObject>());

            var mockService = new Mock<IRandomOrgService>();
            mockService.Setup(m => m.SendRequestAsync(It.IsAny<JObject>())).ReturnsAsync(input);

            var target = new BasicMethod<int>(mockService.Object, mockCallManager.Object);
            var actual = await target.ExecuteAsync(mockRequest.Object);

            actual.Should().Equal(expected);
        }
    }
}
