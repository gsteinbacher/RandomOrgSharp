using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.UnitTest.Tools;
using Obacher.UnitTest.Tools.Mocks;
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
            const int id = 999;

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
    }
}
