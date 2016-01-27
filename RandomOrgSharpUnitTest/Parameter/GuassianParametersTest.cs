using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
{
    [TestClass]
    public class GuassianParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 2;

            // Act
            GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 10001;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 2;

            // Act
            GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMeanLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int mean = -1000001;
            const int standardDeviation = 10000;
            const int significantDigits = 2;

            // Act
            GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMeanGreaterThenMaximumllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int mean = 10000001;
            const int standardDeviation = 10000;
            const int significantDigits = 2;

            // Act
            GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenStandardDeviationLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = -1000001;
            const int significantDigits = 2;

            // Act
            GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenStandardDeviationGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 21;

            // Act
            GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSignificantDigitsLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 1;

            // Act
            GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenSignificantDigitsGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 21;

            // Act
            GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);
        }

        [TestMethod]
        public void WhenAllValuesValid_ExpectValuesSet()
        {
            // Arrange
            const int numberOfItems = 1;
            const int mean = 10000;
            const int standardDeviation = 10000;
            const int significantDigits = 15;
            GuassianParameters result;

            // Act
            result = GuassianParameters.Create(numberOfItems, mean, standardDeviation, significantDigits);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            result.Mean.Should().Equal(mean);
            result.StandardDeviation.Should().Equal(standardDeviation);
            result.SignificantDigits.Should().Equal(significantDigits);
        }
    }
}
