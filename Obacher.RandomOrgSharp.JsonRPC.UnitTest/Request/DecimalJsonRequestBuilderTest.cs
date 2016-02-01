using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Request
{
    [TestClass]
    public class DecimalJsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new DecimalJsonRequestBuilder();
            target.Build(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "DecimalParameters")]
        public void Create_WhenParametersNotTypeOfBlobParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            var target = new DecimalJsonRequestBuilder();
            target.Build(parameters.Object);

            // Assert
        }

        [TestMethod]
        public void Create_WhenParametersCorrect_ExpectJsonReturned()
        {
            // Act
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = 10;

            JObject expected =
                new JObject(
                    new JProperty(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(JsonRpcConstants.DECIMAL_PLACES_PARAMETER_NAME, numberOfdecimalPlaces),
                    new JProperty(JsonRpcConstants.REPLACEMENT_PARAMETER_NAME, true)
                );

            // Act
            var parameters = DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces);
            var target = new DecimalJsonRequestBuilder();
            var actual = target.Build(parameters);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void CanHandle_WhenParametersNull_ExpectException()
        {
            // Arrange

            // Act
            var target = new DecimalJsonRequestBuilder();
            target.CanHandle(null);

            // Assert
        }

        [TestMethod]
        public void CanHandle_WhenParametersMethodTypeIsDecimal_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(m => m.MethodType).Returns(MethodType.Decimal);

            // Act
            var target = new DecimalJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenParametersMethodTypeIsNotDecimal_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(m => m.MethodType).Returns(MethodType.Gaussian);

            // Act
            var target = new DecimalJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Be.False();
        }
    }
}
