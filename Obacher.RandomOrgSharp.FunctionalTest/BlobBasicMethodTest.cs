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
    public class BlobBasicMethodTest
    {
        [TestMethod]
        public void BlobBasicMethod_Execute_ShouldReteurnStringValues()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();
            const int numberToReturn = 2;
            int size = bmt.Random.Next(1, 10) * 8;

            // Act
            var target = new BlobBasicMethod(bmt.AdvisoryDelayHandler, bmt.Service);
            IEnumerable<string> actual = target.GenerateBlobs(numberToReturn, size);

            // Assert
            TestResults(actual, numberToReturn);
        }


        [TestMethod]
        public async Task BlobBasicMethod_ExecuteAsync_ShouldReturnStringValues()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();
            const int numberToReturn = 2;
            int size = bmt.Random.Next(1, 10) * 8;

            // Act
            var target = new BlobBasicMethod(bmt.AdvisoryDelayHandler, bmt.Service);
            IEnumerable<string> actual = await target.GenerateBlobsAsync(numberToReturn, size);

            // Assert
            TestResults(actual, numberToReturn);
        }


        private static void TestResults(IEnumerable<string> results, int numberToReturn)
        {
            results.Should().Not.Be.Null();
            results.Should().Not.Be.Null();
            results.Count().Should().Equal(numberToReturn);
        }
    }
}
