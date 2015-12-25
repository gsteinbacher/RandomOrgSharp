using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
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
            using (new MockCommonParameters())

                // Act
                IntegerParameters.Set(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 10000;
            const int minimumValue = int.MinValue;
            const int maximumValue = 1000;
            using (new MockCommonParameters())

                // Act
                IntegerParameters.Set(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMinimumNumberLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = int.MinValue;
            const int maximumValue = 1000;
            using (new MockCommonParameters())

                // Act
                IntegerParameters.Set(numberOfItems, minimumValue, maximumValue);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMinimumNumberGreaterThenMaximumllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = int.MaxValue;
            const int maximumValue = 1000;
            using (new MockCommonParameters())

                // Act
                IntegerParameters.Set(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMaximumNumberLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = int.MinValue;
            using (new MockCommonParameters())

                // Act
                IntegerParameters.Set(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMaxmimumNumberGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = int.MaxValue;
            using (new MockCommonParameters())

                // Act
                IntegerParameters.Set(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod]
        public void WhenAllValuesValid_ExpectValuesSet()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = 1000;
            const bool allowDuplicates = false;

            IntegerParameters result;
            using (new MockCommonParameters())

                // Act
                result = IntegerParameters.Set(numberOfItems, minimumValue, maximumValue, allowDuplicates);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            result.MinimumValue.Should().Equal(minimumValue);
            result.MaximumValue.Should().Equal(maximumValue);
            result.AllowDuplicates.Should().Equal(allowDuplicates);
        }
    }
}
