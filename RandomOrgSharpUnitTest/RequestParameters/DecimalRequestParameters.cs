using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.RequestParameters;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.RequestParameters
{
    [TestClass]
    public class DecimalRequestParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int numberOfdecimalPlaces = 10;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            new DecimalRequestParameters(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 10001;
            const int numberOfdecimalPlaces = 10;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new DecimalRequestParameters(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfDecimalPlacesLessThenMinimumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = int.MinValue;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new DecimalRequestParameters(numberOfItems, numberOfdecimalPlaces);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfDecimalPlacesGreaterThanMaximumllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = int.MaxValue;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new DecimalRequestParameters(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenApiIsNull_ExpectException()
        {
            SettingsManager.Instance = null;
            new DecimalRequestParameters(1, 1);
        }


        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = 10;
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                    new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, "generateDecimalFractions"),
                    new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME,
                        new JObject(
                            new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                            new JProperty(RandomOrgConstants.JSON_DECIMAL_PLACES_PARAMETER_NAME, numberOfdecimalPlaces),
                            new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, true),
                            new JProperty(RandomOrgConstants.JSON_BASE_NUMBER_PARAMETER_NAME, 10),
                           new JProperty(RandomOrgConstants.APIKEY_KEY, ConfigMocks.MOCK_API_KEY))),
                        new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, 999));


            var random = new Mock<IRandom>();
            random.Setup(m => m.Next()).Returns(id);
            RandomNumberGenerator.Instance = random.Object;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            var target = new DecimalRequestParameters(numberOfItems, numberOfdecimalPlaces);
            var actual = target.CreateJsonRequest();

            actual.Should().Equal(expected);
        }
    }
}
