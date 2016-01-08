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
    public class IntegerJsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new IntegerJsonRequestBuilder();
            target.Create(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "IntegerParameters")]
        public void Create_WhenParametersNotTypeOfIntegerParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            var target = new IntegerJsonRequestBuilder();
            target.Create(parameters.Object);

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
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(RandomOrgConstants.JSON_MINIMUM_VALUE_PARAMETER_NAME, minimumValue),
                    new JProperty(RandomOrgConstants.JSON_MAXIMUM_VALUE_PARAMETER_NAME, maximumValue),
                    new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, allowDuplicates),
                    new JProperty(RandomOrgConstants.JSON_BASE_NUMBER_PARAMETER_NAME, 10)
                );

            // Act
            using (new MockCommonParameters(id))
            {
                var parameters = IntegerParameters.Create(numberOfItems, minimumValue, maximumValue, allowDuplicates);
                var target = new IntegerJsonRequestBuilder();
                var actual = target.Create(parameters);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void CanHandle_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new IntegerJsonRequestBuilder();
            target.CanHandle(null);

            // Assert
        }

        [TestMethod]
        public void CanHandle_WhenMethodTypeIsInteger_ExpectTrue()
        {
            // Arrange
            const bool expected = true;
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(p => p.MethodType).Returns(MethodType.Integer);

            // Act
            var target = new IntegerJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void CanHandle_WhenMethodTypeIsNotInteger_ExpectFalse()
        {
            // Arrange
            const bool expected = false;
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(p => p.MethodType).Returns(MethodType.Blob);

            // Act
            var target = new IntegerJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
