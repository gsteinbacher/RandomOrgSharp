using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Request
{
    [TestClass]
    public class DecimalJsonRequestBuilderTest
    {
        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
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
    }
}
