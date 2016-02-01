using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.JsonRPC.Method;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class IntegerBasicMethodTest
    {
        [TestMethod]
        public void IntegerBasicMethod_Execute_ShouldReturnIntegerValuesInRange()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();
            int numberToReturn = 2;
            int minNumber = bmt.Random.Next(1, 1000);
            int maxNumber = bmt.Random.Next(minNumber + 1, 1000000);
            bool allowDuplicates = bmt.Random.Next(1, 2) == 1;

            // Act
            var target = new IntegerBasicMethod(bmt.AdvisoryDelayHandler, bmt.Service);
            var results = target.GenerateIntegers(numberToReturn, minNumber, maxNumber, allowDuplicates);

            // Assert
            TestResults(results.ToList(), numberToReturn, minNumber, maxNumber, allowDuplicates);
        }


        [TestMethod]
        public async Task IntegerBasicMethod_ExecuteAsync_ShouldReturnIntegerValuesInRange()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();
            int numberToReturn = 2;
            int minNumber = bmt.Random.Next(1, 1000);
            int maxNumber = bmt.Random.Next(minNumber + 1, 1000000);
            bool allowDuplicates = bmt.Random.Next(1, 2) == 1;

            // Act
            var target = new IntegerBasicMethod(bmt.AdvisoryDelayHandler, bmt.Service);
            var results = await target.GenerateIntegersAsync(numberToReturn, minNumber, maxNumber, allowDuplicates);

            // Assert
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