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
    public class UuidJsonRequestBuilderTest
    {
        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            // Arrange
            const int numberOfItems = 1;
            const int id = 999;

            JObject expected =

                new JObject(
                    new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems)
                );

            // Act
            using (new MockCommonParameters(id))
            {
                var parameters = UuidParameters.Create(numberOfItems);
                var target = new UuidJsonRequestBuilder();
                var actual = target.Create(parameters);

                // Assert
                actual.Should().Equal(expected);
            }
        }
    }
}
