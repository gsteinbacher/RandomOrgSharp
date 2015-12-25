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
    public class StringJsonRequestBuilderTest
    {
        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            const string charactersAllowed = "abc";
            const bool allowDuplicates = false;
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(RandomOrgConstants.JSON_LENGTH_PARAMETER_NAME, length),
                    new JProperty(RandomOrgConstants.JSON_CHARACTERS_ALLOWED_PARAMETER_NAME, charactersAllowed),
                    new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, allowDuplicates)
                );

            // Act
            using (new MockCommonParameters(id))
            {
                var parameters = StringParameters.Create(numberOfItems, length, charactersAllowed, allowDuplicates);
                var target = new StringJsonRequestBuilder();
                var actual = target.Create(parameters);

                // Assert
                actual.Should().Equal(expected);
            }
        }
    }
}
