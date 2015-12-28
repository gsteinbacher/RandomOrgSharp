using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.UnitTest.Tools;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Request
{
    [TestClass]
    public class GuassianJsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new GuassianJsonRequestBuilder();
            target.Create(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "GuassianParameters")]
        public void Create_WhenParametersNotTypeOfBlobParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            var target = new GuassianJsonRequestBuilder();
            target.Create(parameters.Object);

            // Assert
        }

        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            // Arrange
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 2;
            const int id = 999;

            JObject expected =

                new JObject(
                    new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(RandomOrgConstants.JSON_MEAN_PARAMETER_NAME, mean),
                    new JProperty(RandomOrgConstants.JSON_STANDARD_DEVIATION_PARAMETER_NAME, standardDeviation),
                    new JProperty(RandomOrgConstants.JSON_SIGNIFICANT_DIGITS_PARAMETER_NAME, significantDigits)
                );

            using (new MockCommonParameters(id))
            {
                // Act
                var parameters = GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);
                var target = new GuassianJsonRequestBuilder();
                var actual = target.Create(parameters);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void CanHandle_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new GuassianJsonRequestBuilder();
            target.CanHandle(null);

            // Assert
        }

        [TestMethod]
        public void CanHandle_WhenMethodTypeIsGuassian_ExpectTrue()
        {
            // Arrange
            const bool expected = true;
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(p => p.MethodType).Returns(MethodType.Gaussian);

            // Act
            var target = new GuassianJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void CanHandle_WhenMethodTypeIsNotGuassian_ExpectFalse()
        {
            // Arrange
            const bool expected = false;
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(p => p.MethodType).Returns(MethodType.Blob);

            // Act
            var target = new GuassianJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
