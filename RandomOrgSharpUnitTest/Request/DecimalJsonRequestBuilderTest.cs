using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Obacher.UnitTest.Tools;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Request
{
    [TestClass]
    public class DecimalJsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new DecimalJsonRequestBuilder();
            target.Create(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "DecimalParameters")]
        public void Create_WhenParametersNotTypeOfBlobParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            var target = new DecimalJsonRequestBuilder();
            target.Create(parameters.Object);

            // Assert
        }

        [TestMethod]
        public void Create_WhenParametersCorrect_ExpectJsonReturned()
        {
            // Act
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = 10;
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(RandomOrgConstants.JSON_DECIMAL_PLACES_PARAMETER_NAME, numberOfdecimalPlaces),
                    new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, true)
                );

            using (new MockCommonParameters(id))
            {
                // Act
                var parameters = DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces);
                var target = new DecimalJsonRequestBuilder();
                var actual = target.Create(parameters);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void CanHandle_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new DecimalJsonRequestBuilder();
            target.CanHandle(null);

            // Assert
        }

        [TestMethod]
        public void CanHandle_WhenMethodTypeIsDecimal_ExpectTrue()
        {
            // Arrange
            const bool expected = true;
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(p => p.MethodType).Returns(MethodType.Decimal);

            // Act
            var target = new DecimalJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void CanHandle_WhenMethodTypeIsNotDecimal_ExpectFalse()
        {
            // Arrange
            const bool expected = false;
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(p => p.MethodType).Returns(MethodType.Blob);

            // Act
            var target = new DecimalJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
