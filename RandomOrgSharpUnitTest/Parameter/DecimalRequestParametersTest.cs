using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
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
            DecimalParameters.Set(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 10001;
            const int numberOfdecimalPlaces = 10;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            DecimalParameters.Set(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfDecimalPlacesLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = int.MinValue;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            DecimalParameters.Set(numberOfItems, numberOfdecimalPlaces);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfDecimalPlacesGreaterThanMaximumllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = int.MaxValue;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            DecimalParameters.Set(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenApiIsNull_ExpectException()
        {
            // Arrange
            SettingsManager.Instance = null;

            // Act
            DecimalParameters.Set(1, 1);
        }

        [TestMethod]
        public void WhenAllValuesValid_ExpectValuesSet()
        {
            // Arrange
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = 100;
            const bool allowDuplicates = false;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            var result = DecimalParameters.Set(numberOfItems, numberOfdecimalPlaces, allowDuplicates);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            result.NumberOfDecimalPlaces.Should().Equal(numberOfdecimalPlaces);
            result.AllowDuplicates.Should().Equal(allowDuplicates);
        }
    }
}
