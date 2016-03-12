using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UuidResponseParserTest
    {
        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "null or empty")]
        public void Parse_WhenNullInput_ShouldThrowException()
        {
            // Arrange
            string input = null;

            // Act
            UuidResponseParser target = new UuidResponseParser();
            target.Parse(input);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "null or empty")]
        public void Parse_WhenEmptyInput_ShouldThrowException()
        {
            // Arrange
            string input = string.Empty;

            // Act
            UuidResponseParser target = new UuidResponseParser();
            var actual = target.Parse(input) as UsageResponseInfo;

            // Assert
        }

        [TestMethod]
        public void Parse_WhenCalled_ShouldAllValuesParsed()
        {
            // Arrange
            const string expectedVersion = "2.0";
            Guid[] expectdData = Enumerable.Range(1, 10).Select(g => Guid.NewGuid()).ToArray();
            DateTime expectedCreationTime = RandomGenerator.GetDate();
            int expectedBitsUsed = RandomGenerator.GetInteger(1);
            int expectedBitsLeft = RandomGenerator.GetInteger(1);
            int expectedRequestsLeft = RandomGenerator.GetInteger(1);
            int expectedAdvisoryDelay = RandomGenerator.GetInteger(1, 1000);
            int expectedId = RandomGenerator.GetInteger(1);

            var input = new JObject(
               new JProperty("jsonrpc", expectedVersion),
               new JProperty("result",
                   new JObject(
                       new JProperty("random",
                           new JObject(
                               new JProperty("data", new JArray(expectdData)),
                               new JProperty("completionTime", expectedCreationTime)
                            )
                        ),
                    new JProperty("bitsUsed", expectedBitsUsed),
                    new JProperty("bitsLeft", expectedBitsLeft),
                    new JProperty("requestsLeft", expectedRequestsLeft),
                    new JProperty("advisoryDelay", expectedAdvisoryDelay)
                    )
                ),
            new JProperty("id", expectedId)
            );


            UuidResponseParser target = new UuidResponseParser();
            var actual = target.Parse(input.ToString()) as DataResponseInfo<Guid>;

            actual.Should().Not.Be.Null();
            actual.Version.Should().Equal(expectedVersion);
            actual.Data.Should().Equal(expectdData);
            actual.CompletionTime.Should().Equal(expectedCreationTime);
            actual.BitsUsed.Should().Equal(expectedBitsUsed);
            actual.BitsLeft.Should().Equal(expectedBitsLeft);
            actual.RequestsLeft.Should().Equal(expectedRequestsLeft);
            actual.Id.Should().Equal(expectedId);
            actual.AdvisoryDelay.Should().Equal(expectedAdvisoryDelay);
        }

        [TestMethod]
        public void CanHandle_WhenUuidMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Uuid);

            // Act
            UuidResponseParser target = new UuidResponseParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanParse_WhenBlobMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Blob);

            // Act
            UuidResponseParser target = new UuidResponseParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }
        [TestMethod]
        public void CanParse_WhenDecimalMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Decimal);

            // Act
            UuidResponseParser target = new UuidResponseParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }
        [TestMethod]
        public void CanParse_WhenGaussianMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Gaussian);

            // Act
            UuidResponseParser target = new UuidResponseParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }
        [TestMethod]
        public void CanParse_WhenIntegerMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Integer);

            // Act
            UuidResponseParser target = new UuidResponseParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }
        [TestMethod]
        public void CanParse_WhenStringMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.String);

            // Act
            UuidResponseParser target = new UuidResponseParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }

        [TestMethod]
        public void CanHandle_WhenUsageMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Usage);

            // Act
            UuidResponseParser target = new UuidResponseParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }

        [TestMethod]
        public void CanHandle_WhenVerifySignatureMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.VerifySignature);

            // Act
            UuidResponseParser target = new UuidResponseParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }
    }
}
