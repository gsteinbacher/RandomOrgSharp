using System;
using System.Collections.Generic;
using System.Dynamic;
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
    public class GenericResponseParserTest
    {
        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "null or empty")]
        public void Parse_WhenNullInput_ShouldThrowException()
        {
            // Arrange
            string input = null;

            // Act
            GenericResponseParser<int> target = new GenericResponseParser<int>();
            target.Parse(input);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "null or empty")]
        public void Parse_WhenEmptyInput_ShouldThrowException()
        {
            // Arrange
            string input = string.Empty;

            // Act
            GenericResponseParser<int> target = new GenericResponseParser<int>();
            var actual = target.Parse(input) as UsageResponseInfo;

            // Assert
        }

        [TestMethod]
        public void Parse_WhenCalled_ShouldAllValuesParsed()
        {
            // Arrange
            const string expectedVersion = "2.0";
            IEnumerable<int> expectdData = Enumerable.Range(1, 10);
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


            GenericResponseParser<int> target = new GenericResponseParser<int>();
            var actual = target.Parse(input.ToString()) as DataResponseInfo<int>;

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
        public void CanParse_WhenBlobMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Blob);

            // Act
            GenericResponseParser<string> target = new GenericResponseParser<string>();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }
        [TestMethod]
        public void CanParse_WhenDecimalMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Decimal);

            // Act
            GenericResponseParser<decimal> target = new GenericResponseParser<decimal>();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }
        [TestMethod]
        public void CanParse_WhenGaussianMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Gaussian);

            // Act
            GenericResponseParser<decimal> target = new GenericResponseParser<decimal>();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }
        [TestMethod]
        public void CanParse_WhenIntegerMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Integer);

            // Act
            GenericResponseParser<int> target = new GenericResponseParser<int>();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }
        [TestMethod]
        public void CanParse_WhenStringMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.String);

            // Act
            GenericResponseParser<string> target = new GenericResponseParser<string>();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenUsageMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Usage);

            // Act
            GenericResponseParser<int> target = new GenericResponseParser<int>();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }

        [TestMethod]
        public void CanHandle_WhenUuidMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Uuid);

            // Act
            GenericResponseParser<int> target = new GenericResponseParser<int>();
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
            GenericResponseParser<int> target = new GenericResponseParser<int>();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }
    }
}
