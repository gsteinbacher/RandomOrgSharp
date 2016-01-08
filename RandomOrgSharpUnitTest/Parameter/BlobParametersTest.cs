using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
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
            using (new MockCommonParameters())

                // Act
                BlobParameters.Create(numberOfItems, size);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 101;
            const int size = 1;
            using (new MockCommonParameters())

                // Act
                BlobParameters.Create(numberOfItems, size);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSizeLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int size = int.MinValue;
            using (new MockCommonParameters())

                // Act
                BlobParameters.Create(numberOfItems, size);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSizeGreaterThenMaximumllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int size = int.MaxValue;
            using (new MockCommonParameters())

                // Act
                BlobParameters.Create(numberOfItems, size);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSizeNotDivisibleBy8_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int size = 20;
            using (new MockCommonParameters())

                // Act
                BlobParameters.Create(numberOfItems, size);
        }

        [TestMethod]
        public void WhenAllValuesValid_ExpectValuesSet()
        {
            // Arrange
            const int numberOfItems = 1;
            const int size = 1000;
            const BlobFormat blobFormat = BlobFormat.Hex;
            BlobParameters result;

            using (new MockCommonParameters())
                // Act
                result = BlobParameters.Create(numberOfItems, size, blobFormat);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            result.Size.Should().Equal(size);
            result.Format.Should().Equal(blobFormat);
        }
    }
}
