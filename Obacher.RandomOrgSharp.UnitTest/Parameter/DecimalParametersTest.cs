using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
{
    [TestClass]
    public class DecimalParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRuntimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int numberOfdecimalPlaces = 10;

            // Act
            DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRuntimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 10001;
            const int numberOfdecimalPlaces = 10;

            // Act
            DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRuntimeException))]
        public void WhenNumberOfDecimalPlacesLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = int.MinValue;

            // Act
            DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRuntimeException))]
        public void WhenNumberOfDecimalPlacesGreaterThanMaximumllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = int.MaxValue;

            // Act
            DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod]
        public void WhenAllValuesValid_ExpectValuesSet()
        {
            // Arrange
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = 15;
            const bool allowDuplicates = false;
            DecimalParameters result;

            // Act
            result = DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces, allowDuplicates);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            result.NumberOfDecimalPlaces.Should().Equal(numberOfdecimalPlaces);
            result.AllowDuplicates.Should().Equal(allowDuplicates);
        }
    }
}
