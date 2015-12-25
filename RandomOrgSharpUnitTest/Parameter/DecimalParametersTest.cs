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
    public class DecimalParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int numberOfdecimalPlaces = 10;
            using (new MockCommonParameters())

                // Act
                DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 10001;
            const int numberOfdecimalPlaces = 10;
            using (new MockCommonParameters())

                // Act
                DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfDecimalPlacesLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = int.MinValue;
            using (new MockCommonParameters())

                // Act
                DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfDecimalPlacesGreaterThanMaximumllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = int.MaxValue;
            using (new MockCommonParameters())

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
            using (new MockCommonParameters())

                // Act
                result = DecimalParameters.Create(numberOfItems, numberOfdecimalPlaces, allowDuplicates);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            result.NumberOfDecimalPlaces.Should().Equal(numberOfdecimalPlaces);
            result.AllowDuplicates.Should().Equal(allowDuplicates);
        }
    }
}
