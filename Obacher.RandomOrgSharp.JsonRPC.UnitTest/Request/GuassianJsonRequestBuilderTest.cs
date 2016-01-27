using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Request
{
    [TestClass]
    public class GuassianJsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new GuassianJsonRequestBuilder();
            target.Build(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "GuassianParameters")]
        public void Create_WhenParametersNotTypeOfBlobParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            var target = new GuassianJsonRequestBuilder();
            target.Build(parameters.Object);

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
                    new JProperty(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(JsonRpcConstants.MEAN_PARAMETER_NAME, mean),
                    new JProperty(JsonRpcConstants.STANDARD_DEVIATION_PARAMETER_NAME, standardDeviation),
                    new JProperty(JsonRpcConstants.SIGNIFICANT_DIGITS_PARAMETER_NAME, significantDigits)
                );

                // Act
                var parameters = GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);
                var target = new GuassianJsonRequestBuilder();
                var actual = target.Build(parameters);

                // Assert
                actual.Should().Equal(expected);
        }
    }
}
