using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.RandomOrgSharp.JsonRPC.Method;
using Obacher.RandomOrgSharp.JsonRPC.Response;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class DecimalBasicMethodTest
    {
        private static Random _random;

        private static AdvisoryDelayHandler _advisoryDelayHandler;

        [ClassInitialize]
        public static void InitializeTests(TestContext context)
        {
            _random = new Random();
            _advisoryDelayHandler = new AdvisoryDelayHandler(new DateTimeWrap());
        }

        [ClassCleanup]
        public static void CleanupTest()
        {
            _random = null;
            _advisoryDelayHandler = null;
        }

        [TestMethod]
        public void DecimalBasicMethod_Execute_ShouldReturnDecimalValuesInRange()
        {
            // Arrange
            const int numberToReturn = 2;
            int numberOfDecimalPlaces = _random.Next(1, 20);
            const bool allowDuplicates = false;

            //RandomOrgApiEmulator service = new RandomOrgApiEmulator();

            // Act
            var target = new DecimalBasicMethod(_advisoryDelayHandler);
            var actual = target.GenerateDecimalsFractions(numberToReturn, numberOfDecimalPlaces, allowDuplicates);

            TestResults(actual.ToList(), numberToReturn, numberOfDecimalPlaces);
        }

        [TestMethod]
        public async Task DecimalBasicMethod_ExecuteAsync_ShouldReturnDecimalValuesInRange()
        {
            // Arrange
            const int numberToReturn = 2;
            int numberOfDecimalPlaces = _random.Next(1, 20);
            const bool allowDuplicates = false;

            //RandomOrgApiEmulator service = new RandomOrgApiEmulator();

            // Act
            var target = new DecimalBasicMethod(_advisoryDelayHandler);
            var actual = await target.GenerateDecimalsFractionsAsync(numberToReturn, numberOfDecimalPlaces, allowDuplicates);

            // Assert
            TestResults(actual.ToList(), numberToReturn, numberOfDecimalPlaces);
        }


        private static void TestResults(IList<decimal> results, int numberToReturn, int numberOfDecimalPlaces)
        {
            results.Should().Not.Be.Null();
            results.Count().Should().Equal(numberToReturn);

            foreach (var result in results)
            {
                result.Should().Be.GreaterThan(0m);
                result.Should().Be.LessThan(1m);
                result.ToString(CultureInfo.InvariantCulture).Length.Should().Be.LessThanOrEqualTo(numberOfDecimalPlaces + 2);  // the "+2" takes into account the "0." at the beginning of returned value
            }
        }
    }
}
