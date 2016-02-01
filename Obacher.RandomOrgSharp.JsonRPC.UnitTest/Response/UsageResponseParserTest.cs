using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Response;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Response
{
    [TestClass]
    public class UsageResponseParserTest
    {
        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "null or empty")]
        public void Parser_WhenNullInput_ShouldThrowException()
        {
            // Arrange
            string input = null;

            // Act
            UsageResponseParser target = new UsageResponseParser();
            target.Parse(input);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "null or empty")]
        public void Parser_WhenEmptyInput_ShouldThrowException()
        {
            // Arrange
            string input = string.Empty;

            // Act
            UsageResponseParser target = new UsageResponseParser();
            var actual = target.Parse(input) as UsageResponseInfo;

            // Assert
        }

        [TestMethod]
        public void Parser_WhenCalled_ShouldAllValuesParsed()
        {
            // Arrange
            const string expectedVersion = "2.0";
            const StatusType expectedStatus = StatusType.Paused;
            DateTime expectedCreationTime = RandomGenerator.GetDate();
            int expectedBitsLeft = RandomGenerator.GetInteger(1);
            int expectedRequestsLeft = RandomGenerator.GetInteger(1);
            int expectedTotalBits = RandomGenerator.GetInteger(1);
            int expectedTotalRequest = RandomGenerator.GetInteger(1);
            int expectedId = RandomGenerator.GetInteger(1);
            const int expectedAdvisoryDelay = 0;

            var input = new JObject(
                new JProperty("jsonrpc", expectedVersion),
                new JProperty("result",
                    new JObject(
                        new JProperty("status", expectedStatus.ToString().ToLower()),
                        new JProperty("creationTime", expectedCreationTime),
                        new JProperty("bitsLeft", expectedBitsLeft),
                        new JProperty("requestsLeft", expectedRequestsLeft),
                        new JProperty("totalBits", expectedTotalBits),
                        new JProperty("totalRequests", expectedTotalRequest)
                        )),
                new JProperty("id", expectedId)
                );


            UsageResponseParser target = new UsageResponseParser();
            var actual = target.Parse(input.ToString()) as UsageResponseInfo;

            actual.Should().Not.Be.Null();
            actual.Version.Should().Equal(expectedVersion);
            actual.Status.Should().Equal(expectedStatus);
            actual.CreationTime.Should().Equal(expectedCreationTime);
            actual.BitsLeft.Should().Equal(expectedBitsLeft);
            actual.RequestsLeft.Should().Equal(expectedRequestsLeft);
            actual.TotalBits.Should().Equal(expectedTotalBits);
            actual.TotalRequests.Should().Equal(expectedTotalRequest);
            actual.Id.Should().Equal(expectedId);
            actual.AdvisoryDelay.Should().Equal(expectedAdvisoryDelay);
        }

        [TestMethod]
        public void Parser_WhenStatusIsRunning_ShouldStatusParsedCorrectly()
        {
            // Arrange
            const StatusType expectedStatus = StatusType.Running;

            var input = new JObject(
                new JProperty("result",
                    new JObject(
                        new JProperty("status", expectedStatus.ToString().ToLower())
                        ))
                );


            UsageResponseParser target = new UsageResponseParser();
            var actual = target.Parse(input.ToString()) as UsageResponseInfo;

            actual.Should().Not.Be.Null();
            actual.Status.Should().Equal(expectedStatus);
        }

        [TestMethod]
        public void Parser_WhenStatusIsStopped_ShouldStatusParsedCorrectly()
        {
            // Arrange
            const StatusType expectedStatus = StatusType.Stopped;

            var input = new JObject(
                new JProperty("result",
                    new JObject(
                        new JProperty("status", expectedStatus.ToString().ToLower())
                        ))
                );


            UsageResponseParser target = new UsageResponseParser();
            var actual = target.Parse(input.ToString()) as UsageResponseInfo;

            actual.Should().Not.Be.Null();
            actual.Status.Should().Equal(expectedStatus);
        }

        [TestMethod]
        public void Parser_WhenStatusIsUnknown_ShouldStatusParsedCorrectly()
        {
            // Arrange
            const StatusType expectedStatus = StatusType.Unknown;

            var input = new JObject(
                new JProperty("result",
                    new JObject(
                        new JProperty("status", expectedStatus.ToString().ToLower())
                        ))
                );


            UsageResponseParser target = new UsageResponseParser();
            var actual = target.Parse(input.ToString()) as UsageResponseInfo;

            actual.Should().Not.Be.Null();
            actual.Status.Should().Equal(expectedStatus);
        }

        [TestMethod]
        public void CanHandle_WhenUsage_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Usage);

            // Act
            UsageResponseParser target = new UsageResponseParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenNotUsage_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Blob);

            // Act
            UsageResponseParser target = new UsageResponseParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }
    }
}
