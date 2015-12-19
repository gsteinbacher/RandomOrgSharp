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
    public class IntegerRequestParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int minimumValue = 10;
            const int maximumValue = 1000;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 10000;
            const int minimumValue = int.MinValue;
            const int maximumValue = 1000;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMinimumNumberLessThenMinimumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int minimumValue = int.MinValue;
            const int maximumValue = 1000;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMinimumNumberGreaterThenMaximumllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int minimumValue = int.MaxValue;
            const int maximumValue = 1000;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMaximumNumberLessThenMinimumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = int.MinValue;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMaxmimumNumberGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = int.MaxValue;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenApiIsNull_ExpectException()
        {
            SettingsManager.Instance = null;
            new IntegerRequestParameters(1, 1, 1);
        }


        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = 1000;
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                    new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, "generateIntegers"),
                    new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME,
                        new JObject(
                            new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                            new JProperty(RandomOrgConstants.JSON_MINIMUM_VALUE_PARAMETER_NAME, minimumValue),
                            new JProperty(RandomOrgConstants.JSON_MAXIMUM_VALUE_PARAMETER_NAME, maximumValue),
                            new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, true),
                            new JProperty(RandomOrgConstants.JSON_BASE_NUMBER_PARAMETER_NAME, 10),
                           new JProperty(RandomOrgConstants.APIKEY_KEY, ConfigMocks.MOCK_API_KEY))),
                        new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, 999));


            var random = new Mock<IRandom>();
            random.Setup(m => m.Next()).Returns(id);
            RandomNumberGenerator.Instance = random.Object;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            var target = new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
            var actual = target.CreateJsonRequest();

            actual.Should().Equal(expected);
        }
    }
}
