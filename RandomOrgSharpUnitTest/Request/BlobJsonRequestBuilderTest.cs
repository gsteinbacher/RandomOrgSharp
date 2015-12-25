using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Request
{
    [TestClass]
    public class BlobJsonRequestBuilderTest
    {
        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            // Arrange
            const int numberOfItems = 1;
            const int size = 8;
            const BlobFormat format = BlobFormat.Hex;
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(RandomOrgConstants.JSON_SIZE_PARAMETER_NAME, size),
                    new JProperty(RandomOrgConstants.JSON_FORMAT_PARAMETER_NAME, format.ToString().ToLowerInvariant())
                );

            using (new MockCommonParameters(id))
            {
                var parameters = BlobParameters.Create(numberOfItems, size, format);
                var target = new BlobJsonRequestBuilder();
                var actual = target.Create(parameters);

                // Assert
                actual.Should().Equal(expected);
            }

        }
    }
}
