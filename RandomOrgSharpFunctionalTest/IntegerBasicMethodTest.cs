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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class IntegerBasicMethodTest
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
        public void IntegerBasicMethod_Execute_ShouldReturnIntegerValuesInRange()
        {
            // Arrange
            int numberToReturn = 2;
            int minNumber = _random.Next(1, 1000);
            int maxNumber = _random.Next(minNumber + 1, 1000000);
            bool allowDuplicates = _random.Next(1, 2) == 1;

            // Act
            var target = new IntegerBasicMethod(_advisoryDelayHandler);
            var results = target.GenerateIntegers(numberToReturn, minNumber, maxNumber, allowDuplicates);

            // Assert
            TestResults(results.ToList(), numberToReturn, minNumber, maxNumber, allowDuplicates);
        }


        [TestMethod]
        public async Task IntegerBasicMethod_ExecuteAsync_ShouldReturnIntegerValuesInRange()
        {
            int numberToReturn = 2;
            int minNumber = _random.Next(1, 1000);
            int maxNumber = _random.Next(minNumber + 1, 1000000);
            bool allowDuplicates = _random.Next(1, 2) == 1;

            // Act
            var target = new IntegerBasicMethod(_advisoryDelayHandler);
            var results = await target.GenerateIntegersAsync(numberToReturn, minNumber, maxNumber, allowDuplicates);

            TestResults(results.ToList(), numberToReturn, minNumber, maxNumber, allowDuplicates);
        }

        private static void TestResults(IList<int> results, int numberToReturn, int minNumber,
                                        int maxNumber, bool allowDuplicates)
        {
            results.Should().Not.Be.Null();
            results.Count().Should().Equal(numberToReturn);

            // Ensure there are no duplicates
            if (!allowDuplicates)
                CollectionAssert.AllItemsAreUnique(results.ToArray());

            foreach (var result in results)
                result.Should().Be.InRange(minNumber, maxNumber);
        }
    }
}