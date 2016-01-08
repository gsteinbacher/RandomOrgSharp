using System;
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
    public class GuassianBasicMethodTest
    {
        private Random _random;

        [TestInitialize]
        public void InitializeTests()
        {
            _random = new Random();
        }

        [TestMethod]
        public void GuassianBasicMethodTest_Execute_ShouldReturnDoubleValuesInRange()
        {
            // Arrange
            int numberToReturn = 2;
            int mean = _random.Next(-1000000, 1000000);
            int standardDeviation = _random.Next(-1000000, 1000000);
            int signifantDigits = _random.Next(2, 20);

            // Act
            var target = new GuassianMethod();
            var results = target.GenerateGuassians(numberToReturn, mean, standardDeviation, signifantDigits);

            // Assert
            TestResults(results, numberToReturn);
        }

        [TestMethod]
        public async Task GuassianBasicMethod_ExecuteAsync_ShouldReturnDoubleValuesInRange()
        {
            // Arrange
            int numberToReturn = 2;
            int mean = _random.Next(-1000000, 1000000);
            int standardDeviation = _random.Next(-1000000, 1000000);
            int signifantDigits = _random.Next(2, 20);

            // Act
            var target = new GuassianMethod();
            var results = await target.GenerateGuassiansAsync(numberToReturn, mean, standardDeviation, signifantDigits);

            // Assert
            TestResults(results, numberToReturn);
        }


        private static void TestResults(DataResponseInfo<decimal> results, int numberToReturn)
        {
            results.Should().Not.Be.Null();
            results.Data.Count().Should().Equal(numberToReturn);
        }
    }
}
