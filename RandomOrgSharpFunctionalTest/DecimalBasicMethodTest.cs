using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.Method;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class DecimalBasicMethodTest
    {
        private Random _random;

        [TestInitialize]
        public void InitializeTests()
        {
            _random = new Random();
        }

        [TestMethod]
        public void DecimalBasicMethod_Execute_ShouldReturnDecimalValuesInRange()
        {
            // Arrange
            const int numberToReturn = 2;
            int numberOfDecimalPlaces = _random.Next(1, 20);
            const bool allowDuplicates = false;

            // Act
            var target = new DecimalMethod();
            var results = target.GenerateDecimalFractions(numberToReturn, numberOfDecimalPlaces, allowDuplicates);

            // Assert
            TestResults(results, numberToReturn, numberOfDecimalPlaces);
        }

        [TestMethod]
        public async Task DecimalBasicMethod_ExecuteAsync_ShouldReturnDecimalValuesInRange()
        {
            // Arrange
            const int numberToReturn = 2;
            int numberOfDecimalPlaces = _random.Next(1, 20);
            const bool allowDuplicates = false;

            // Act
            var target = new DecimalMethod();
            var results = await target.GenerateDecimalFractionsAsync(numberToReturn, numberOfDecimalPlaces, allowDuplicates);

            // Assert
            TestResults(results, numberToReturn, numberOfDecimalPlaces);
        }


        private static void TestResults(DataResponseInfo<decimal> results, int numberToReturn, int numberOfDecimalPlaces)
        {
            results.Should().Not.Be.Null();
            results.Data.Count().Should().Equal(numberToReturn);

            foreach (var result in results.Data)
            {
                result.Should().Be.GreaterThan(0m);
                result.Should().Be.LessThan(1m);
                result.ToString(CultureInfo.InvariantCulture).Length.Should().Be.LessThanOrEqualTo(numberOfDecimalPlaces + 2);  // the "+2" takes into account the "0." at the beginning of returned value
            }
        }
    }
}
