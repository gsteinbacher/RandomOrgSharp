using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.RequestParameters
{
    [TestClass]
    public class StringJsonRequestBuilderTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int length = 10;
            const string charactersAllowed = "abc";
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            new StringRequestParameters(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 10001;
            const int length = 10;
            const string charactersAllowed = "abc";
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new StringRequestParameters(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenLengthLessThenMinimumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int length = 0;
            const string charactersAllowed = "abc";
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new StringRequestParameters(numberOfItems, length, charactersAllowed);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenLengthGreaterThenMaximumllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int length = 21;
            const string charactersAllowed = "abc";
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new StringRequestParameters(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenCharactersAllowedIsEmpty_ExpectException()
        {
            const int numberOfItems = 1;
            const int length = 10;
            string charactersAllowed = string.Empty;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new StringRequestParameters(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenCharactersAllowedLengthGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int length = 10;
            string charactersAllowed = new string('a', 81);
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new StringRequestParameters(numberOfItems, length, charactersAllowed);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenApiIsNull_ExpectException()
        {
            SettingsManager.Instance = null;
            new StringRequestParameters(1, 1, "");
        }


        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            const int numberOfItems = 1;
            const int length = 10;
            const string charactersAllowed = "abc";
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                    new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, RandomOrgConstants.STRING_METHOD),
                    new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME,
                        new JObject(
                            new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                            new JProperty(RandomOrgConstants.JSON_LENGTH_PARAMETER_NAME, length),
                            new JProperty(RandomOrgConstants.JSON_CHARACTERS_ALLOWED_PARAMETER_NAME, charactersAllowed),
                            new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, true),
                           new JProperty(RandomOrgConstants.APIKEY_KEY, ConfigMocks.MOCK_API_KEY))),
                        new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, 999));


            var random = new Mock<IRandom>();
            random.Setup(m => m.Next()).Returns(id);
            RandomNumberGenerator.Instance = random.Object;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            var target = new StringRequestParameters(numberOfItems, length, charactersAllowed);
            var actual = target.CreateJsonRequest();

            actual.Should().Equal(expected);
        }
    }
}
