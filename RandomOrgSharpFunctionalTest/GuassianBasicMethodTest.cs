using System;
using System.Collections.Generic;
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
    public class GuassianBasicMethodTest
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
        public void GuassianBasicMethodTest_Execute_ShouldReturnDoubleValuesInRange()
        {
            // Arrange
            int numberToReturn = 2;
            int mean = _random.Next(-1000000, 1000000);
            int standardDeviation = _random.Next(-1000000, 1000000);
            int signifantDigits = _random.Next(2, 20);

            // Act
            var target = new GuassianBasicMethod(_advisoryDelayHandler);
            var results = target.GenerateGuassians(numberToReturn, mean, standardDeviation, signifantDigits);

            // Assert
            TestResults(results.ToList(), numberToReturn);
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
            var target = new GuassianBasicMethod(_advisoryDelayHandler);
            var results = await target.GenerateGuassiansAsync(numberToReturn, mean, standardDeviation, signifantDigits);

            // Assert
            TestResults(results.ToList(), numberToReturn);
        }


        private static void TestResults(IList<decimal> results, int numberToReturn)
        {
            results.Should().Not.Be.Null();
            results.Count().Should().Equal(numberToReturn);
        }
    }
}
