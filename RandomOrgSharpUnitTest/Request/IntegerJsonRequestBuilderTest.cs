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
    public class IntegerJsonRequestBuilderTest
    {

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
    }
}
