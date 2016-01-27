using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
{
    [TestClass]
    public class IntegerParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int minimumValue = 10;
            const int maximumValue = 1000;

            // Act
            IntegerParameters.Create(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 10000;
            const int minimumValue = int.MinValue;
            const int maximumValue = 1000;

            // Act
            IntegerParameters.Create(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMinimumNumberLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = int.MinValue;
            const int maximumValue = 1000;

            // Act
            IntegerParameters.Create(numberOfItems, minimumValue, maximumValue);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMinimumNumberGreaterThenMaximumllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = int.MaxValue;
            const int maximumValue = 1000;

            // Act
            IntegerParameters.Create(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMaximumNumberLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = int.MinValue;

            // Act
            IntegerParameters.Create(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMaxmimumNumberGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = int.MaxValue;

            // Act
            IntegerParameters.Create(numberOfItems, minimumValue, maximumValue);
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

            // Act
            result = IntegerParameters.Create(numberOfItems, minimumValue, maximumValue, allowDuplicates);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            result.MinimumValue.Should().Equal(minimumValue);
            result.MaximumValue.Should().Equal(maximumValue);
            result.AllowDuplicates.Should().Equal(allowDuplicates);
        }
    }
}
