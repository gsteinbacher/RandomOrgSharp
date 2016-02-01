using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.JsonRPC.Method;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class GuassianBasicMethodTest
    {

        [TestMethod]
        public void GuassianBasicMethodTest_Execute_ShouldReturnDoubleValuesInRange()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();
            int numberToReturn = 2;
            int mean = bmt.Random.Next(-1000000, 1000000);
            int standardDeviation = bmt.Random.Next(-1000000, 1000000);
            int signifantDigits = bmt.Random.Next(2, 20);

            // Act
            var target = new GuassianBasicMethod(bmt.AdvisoryDelayHandler, bmt.Service);
            var results = target.GenerateGuassians(numberToReturn, mean, standardDeviation, signifantDigits);

            // Assert
            TestResults(results.ToList(), numberToReturn);
        }

        [TestMethod]
        public async Task GuassianBasicMethod_ExecuteAsync_ShouldReturnDoubleValuesInRange()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();
            int numberToReturn = 2;
            int mean = bmt.Random.Next(-1000000, 1000000);
            int standardDeviation = bmt.Random.Next(-1000000, 1000000);
            int signifantDigits = bmt.Random.Next(2, 20);

            // Act
            var target = new GuassianBasicMethod(bmt.AdvisoryDelayHandler, bmt.Service);
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
