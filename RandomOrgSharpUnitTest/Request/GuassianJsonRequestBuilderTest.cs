using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.RequestParameters
{
    [TestClass]
    public class GuassianJsonRequestBuilderTest
    {
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
                var parameters = GuassianParameters.Set(numberOfItems, mean, standardDeviation, significantDigits);
                var target = new GuassianJsonRequestBuilder();
                var actual = target.Create(parameters);

                // Assert
                actual.Should().Equal(expected);
            }
        }
    }
}
