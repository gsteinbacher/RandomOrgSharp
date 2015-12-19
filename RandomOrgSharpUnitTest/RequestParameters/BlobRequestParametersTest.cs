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
    public class BlobRequestParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int size = 10;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            new BlobRequestParameters(numberOfItems, size);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 101;
            const int size = 1;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new BlobRequestParameters(numberOfItems, size);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSizeLessThenMinimumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int size = int.MinValue;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new BlobRequestParameters(numberOfItems, size);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSizeGreaterThenMaximumllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int size = int.MaxValue;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            new BlobRequestParameters(numberOfItems, size);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenApiIsNull_ExpectException()
        {
            SettingsManager.Instance = null;
            new BlobRequestParameters(1, 1);
        }


        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            const int numberOfItems = 1;
            const int size = 8;
            BlobFormat format = BlobFormat.Base64;
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                    new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, RandomOrgConstants.BLOB_METHOD),
                    new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME,
                        new JObject(
                            new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                            new JProperty(RandomOrgConstants.JSON_SIZE_PARAMETER_NAME, size),
                            new JProperty(RandomOrgConstants.JSON_FORMAT_PARAMETER_NAME, format.ToString().ToLowerInvariant()),
                           new JProperty(RandomOrgConstants.APIKEY_KEY, ConfigMocks.MOCK_API_KEY))),
                        new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, 999));


            var random = new Mock<IRandom>();
            random.Setup(m => m.Next()).Returns(id);
            RandomNumberGenerator.Instance = random.Object;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            var target = new BlobRequestParameters(numberOfItems, size, format);
            var actual = target.CreateJsonRequest();

            actual.Should().Equal(expected);
        }
    }
}
