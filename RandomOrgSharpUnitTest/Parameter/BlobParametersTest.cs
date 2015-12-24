using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
{
    [TestClass]
    public class BlobParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int size = 10;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            BlobParameters.Set(numberOfItems, size);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 101;
            const int size = 1;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            BlobParameters.Set(numberOfItems, size);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSizeLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int size = int.MinValue;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            BlobParameters.Set(numberOfItems, size);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSizeGreaterThenMaximumllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int size = int.MaxValue;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            BlobParameters.Set(numberOfItems, size);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenApiIsNull_ExpectException()
        {
            // Arrange
            SettingsManager.Instance = null;

            // Act
            BlobParameters.Set(1, 1);
        }

        [TestMethod]
        public void WhenAllValuesValid_ExpectValuesSet()
        {
            // Arrange
            const int numberOfItems = 1;
            const int size = 1000;
            const BlobFormat blobFormat = BlobFormat.Hex;
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;

            // Act
            var result = BlobParameters.Set(numberOfItems, size, blobFormat);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            result.Size.Should().Equal(size);
            result.Format.Should().Equal(blobFormat);
        }
    }
}
