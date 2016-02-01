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
    public class IntegerJsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new IntegerJsonRequestBuilder();
            target.Build(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "IntegerParameters")]
        public void Create_WhenParametersNotTypeOfIntegerParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            var target = new IntegerJsonRequestBuilder();
            target.Build(parameters.Object);

            // Assert
        }

        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = 1000;
            const bool allowDuplicates = false;

            JObject expected =
                new JObject(
                    new JProperty(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(JsonRpcConstants.MINIMUM_VALUE_PARAMETER_NAME, minimumValue),
                    new JProperty(JsonRpcConstants.MAXIMUM_VALUE_PARAMETER_NAME, maximumValue),
                    new JProperty(JsonRpcConstants.REPLACEMENT_PARAMETER_NAME, allowDuplicates),
                    new JProperty(JsonRpcConstants.BASE_NUMBER_PARAMETER_NAME, 10)
                );

            // Act
            var parameters = IntegerParameters.Create(numberOfItems, minimumValue, maximumValue, allowDuplicates);
            var target = new IntegerJsonRequestBuilder();
            var actual = target.Build(parameters);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void CanHandle_WhenParametersNull_ExpectException()
        {
            // Arrange

            // Act
            var target = new IntegerJsonRequestBuilder();
            target.CanHandle(null);

            // Assert
        }

        [TestMethod]
        public void CanHandle_WhenParametersMethodTypeIsInteger_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(m => m.MethodType).Returns(MethodType.Integer);

            // Act
            var target = new IntegerJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenParametersMethodTypeIsNoInteger_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(m => m.MethodType).Returns(MethodType.Decimal);

            // Act
            var target = new IntegerJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Be.False();
        }
    }
}
