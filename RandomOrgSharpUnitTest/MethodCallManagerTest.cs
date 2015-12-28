using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.Framework.Common.SystemWrapper.Interface;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;
using Obacher.UnitTest.Tools;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest
{
    [TestClass]
    public class MethodCallManagerTest
    {
        [TestMethod, ExceptionExpected(typeof(RandomOrgException), "400")]
        public void CanSendRequest_WhenCodeIs400AndApiKeyMatches_ExpectException()
        {
            // Arrange
            const string apiKey = ConfigMocks.MOCK_API_KEY;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_code", 400);
            UnitTestHelper.SetPrivateProperty(target, "_message", "Putting 400 in message to ensure confirmation");
            UnitTestHelper.SetPrivateProperty(target, "_apiKey", apiKey);
            target.CanSendRequest();

            // Assert
        }

        [TestMethod]
        public void CanSendRequest_WhenCodeIs400AndApiKeyDoesNotMatches_ExpectNoException()
        {
            // Arrange
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_code", 400);
            UnitTestHelper.SetPrivateProperty(target, "_apiKey", "differentApiKey");
            target.CanSendRequest();
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgException), "402")]
        public void CanSendRequest_WhenCodeIs402AndDatesMatch_ExpectException()
        {
            // Arrange
            IDateTime responseDate = new DateTimeWrap(2015, 10, 15);
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(responseDate);

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_code", 402);
            UnitTestHelper.SetPrivateProperty(target, "_message", "Putting 402 in message to ensure confirmation");
            UnitTestHelper.SetPrivateProperty(target, "_lastResponse", responseDate);
            target.CanSendRequest();

            // Assert
        }

        [TestMethod]
        public void CanSendRequest_WhenCodeIs402AndDatesDoNotMatch_ExpectNoException()
        {
            // Arrange
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(new DateTimeWrap(2015, 10, 15));

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_code", 402);
            UnitTestHelper.SetPrivateProperty(target, "_lastResponse", new DateTimeWrap(2015, 10, 14));
            target.CanSendRequest();

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgException), "403")]
        public void CanSendRequest_WhenCodeIs403AndDatesMatch_ExpectException()
        {
            // Arrange
            IDateTime responseDate = new DateTimeWrap(2015, 10, 15);
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(responseDate);

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_code", 403);
            UnitTestHelper.SetPrivateProperty(target, "_message", "Putting 403 in message to ensure confirmation");
            UnitTestHelper.SetPrivateProperty(target, "_lastResponse", responseDate);
            target.CanSendRequest();

            // Assert
        }

        [TestMethod]
        public void CanSendRequest_WhenCodeIs403AndDatesDoNotMatch_ExpectCodeSetToZero()
        {
            // Arrange
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(new DateTimeWrap(2015, 10, 15));

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_code", 403);
            UnitTestHelper.SetPrivateProperty(target, "_lastResponse", new DateTimeWrap(2015, 10, 14));
            target.CanSendRequest();
            var codeValue = UnitTestHelper.GetPrivateProperty<int>(target, "_code");

            // Assert
            codeValue.Should().Equal(0);
        }

        [TestMethod]
        public void Delay_WhenDelay_ExpectNoException()
        {
            // Arrange
            var utcNow = new DateTimeWrap(2015, 10, 15);
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(utcNow);

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_advisoryDelay", utcNow.Ticks - 1);
            target.Delay();
        }

        [TestMethod]
        public void SetAdvisoryDelay_WhenCalled_ExpectDelayToMatch()
        {
            // Arrange
            var delay = 1234567;
            var utcNow = new DateTimeWrap(2015, 10, 15);
            long expected = utcNow.Ticks + delay;
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(utcNow);

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            target.SetAdvisoryDelay(delay);

            var actual = UnitTestHelper.GetPrivateProperty<long>(target, "_advisoryDelay");

            actual.Should().Equal(expected);
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgException), "Method not found")]
        public void ThrowExceptionOnError_WhenCalled_ExpectException()
        {
            // Arrange
            JObject response = JObject.Parse(@"{
    jsonrpc: '2.0',
    error: {
               code: -32601,
               message: 'Method not found',
                data: []
    },
    id: 18197
}");

            var utcNow = new DateTimeWrap(2015, 10, 15);
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(utcNow);

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            target.ThrowExceptionOnError(response);
        }

        [TestMethod]
        public void ThrowExceptionOnError_WhenErrorIs400_ExpectApiKeySet()
        {
            // Arrange
            JObject response = JObject.Parse(@"{
    jsonrpc: '2.0',
    error: {
               code: 400,
               message: 'Method not found',
                data: null
    },
    id: 18197
}");

            var utcNow = new DateTimeWrap(2015, 10, 15);
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(utcNow);

            const string expected = ConfigMocks.MOCK_API_KEY;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_apiKey", null);         // Ensure apiKey starts out null
            try
            {
                target.ThrowExceptionOnError(response);
            }
            catch (RandomOrgException)
            {
                // Ignore RandomOrgException so we can check the api value
            }

            var actual = UnitTestHelper.GetPrivateProperty<string>(target, "_apiKey");
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void ThrowExceptionOnError_WhenErrorIs402_ExpectLastResponseSet()
        {
            // Arrange
            JObject response = JObject.Parse(@"{
    jsonrpc: '2.0',
    error: {
               code: 402,
               message: 'Method not found',
               data: ['1','2']

    },
    id: 18197
}");

            var utcNow = new DateTimeWrap(2015, 10, 15);
            var expected = utcNow;
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(utcNow);

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_lastResponse", new DateTimeWrap(DateTime.MinValue));         // Ensure apiKey starts out null
            try
            {
                target.ThrowExceptionOnError(response);
            }
            catch (RandomOrgException)
            {
                // Ignore RandomOrgException so we can check the api value
            }

            var actual = UnitTestHelper.GetPrivateProperty<IDateTime>(target, "_lastResponse");
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void ThrowExceptionOnError_WhenErrorIs403_ExpectLastResponseSet()
        {
            // Arrange
            JObject response = JObject.Parse(@"{
    jsonrpc: '2.0',
    error: {
               code: 403,
               message: 'Method not found',
               data: ['1','2']

    },
    id: 18197
}");

            var utcNow = new DateTimeWrap(2015, 10, 15);
            var expected = utcNow;
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(utcNow);

            // Act
            MethodCallManager target = new MethodCallManager(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_lastResponse", new DateTimeWrap(DateTime.MinValue));         // Ensure apiKey starts out null
            try
            {
                target.ThrowExceptionOnError(response);
            }
            catch (RandomOrgException)
            {
                // Ignore RandomOrgException so we can check the api value
            }

            var actual = UnitTestHelper.GetPrivateProperty<IDateTime>(target, "_lastResponse");
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void VerifyResponse_WhenIdsMatch_ExpectNoException()
        {
            const int id = 1234;
            Mock<IParameters> requestMock = new Mock<IParameters>();
            requestMock.Setup(p => p.Id).Returns(id);
            Mock<IResponse> responseMock = new Mock<IResponse>();
            responseMock.Setup(p => p.Id).Returns(id);

            MethodCallManager target = new MethodCallManager();
            target.VerifyResponse(requestMock.Object, responseMock.Object);
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRunTimeException), "Id passed into the request does not match the Id returned in response")]
        public void VerifyResponse_WhenIdsDotNotMatch_ExpeNoException()
        {
            Mock<IParameters> requestMock = new Mock<IParameters>();
            requestMock.Setup(p => p.Id).Returns(1234);
            Mock<IResponse> responseMock = new Mock<IResponse>();
            responseMock.Setup(p => p.Id).Returns(9786);

            MethodCallManager target = new MethodCallManager();
            target.VerifyResponse(requestMock.Object, responseMock.Object);
        }

        [TestMethod]
        public void Dispose_WhenCalled_ExpectNoException()
        {
            using (MethodCallManager target = new MethodCallManager())
            {
            }

        }
    }
}
