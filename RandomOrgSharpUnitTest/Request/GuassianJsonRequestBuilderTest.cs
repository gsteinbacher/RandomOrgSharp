using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.RequestParameters
{
    [TestClass]
    public class GuassianJsonRequestBuilderTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 2;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            new GuassianRequestParameters(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 10001;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 2;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new GuassianRequestParameters(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMeanLessThenMinimumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int mean = -1000001;
            const int standardDeviation = 10000;
            const int significantDigits = 2;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new GuassianRequestParameters(numberOfItems, mean, standardDeviation, significantDigits);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMeanGreaterThenMaximumllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int mean = 10000001;
            const int standardDeviation = 10000;
            const int significantDigits = 2;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new GuassianRequestParameters(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenStandardDeviationLessThenMinimumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = -1000001;
            const int significantDigits = 2;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new GuassianRequestParameters(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenStandardDeviationGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 21;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new GuassianRequestParameters(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSignificantDigitsLessThenMinimumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 1;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new GuassianRequestParameters(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSignificantDigitsGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 21;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new GuassianRequestParameters(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenApiIsNull_ExpectException()
        {
            SettingsManager.Instance = null;
            new GuassianRequestParameters(1, 1, 1, 2);
        }


        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 2;
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                    new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, RandomOrgConstants.GAUSSIAN_METHOD),
                    new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME,
                        new JObject(
                            new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                            new JProperty(RandomOrgConstants.JSON_MEAN_PARAMETER_NAME, mean),
                            new JProperty(RandomOrgConstants.JSON_STANDARD_DEVIATION_PARAMETER_NAME, standardDeviation),
                            new JProperty(RandomOrgConstants.JSON_SIGNIFICANT_DIGITS_PARAMETER_NAME, significantDigits),
                           new JProperty(RandomOrgConstants.APIKEY_KEY, ConfigMocks.MOCK_API_KEY))),
                        new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, 999));


            var random = new Mock<IRandom>();
            random.Setup(m => m.Next()).Returns(id);
            RandomNumberGenerator.Instance = random.Object;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            var target = new GuassianRequestParameters(numberOfItems, mean, standardDeviation, significantDigits);
            var actual = target.CreateJsonRequest();

            actual.Should().Equal(expected);
        }
    }
}
