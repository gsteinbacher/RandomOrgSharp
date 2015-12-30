using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Method;
using Obacher.RandomOrgSharp.Response;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class BlobBasicMethodTest
    {
        private Random _random;

        [TestInitialize]
        public void InitializeTests()
        {
            _random = new Random();
        }

        [TestMethod]
        public void BlobBasicMethod_Execute_ShouldReteurnStringValues()
        {
            // Arrange
            const int numberToReturn = 2;
            int size = _random.Next(1, 10) * 8;

            // Act
            var target = new BlobMethod();
            var results = target.GenerateBlobs(numberToReturn, size);

            // Assert
            TestResults(results, numberToReturn);
        }


        [TestMethod]
        public async Task BlobBasicMethod_ExecuteAsync_ShouldReturnStringValues()
        {
            // Arrange
            const int numberToReturn = 2;
            int size = _random.Next(1, 10) * 8;

            // Act
            var target = new BlobMethod();
            var results = await target.GenerateBlobsAsync(numberToReturn, size);

            // Assert
            TestResults(results, numberToReturn);
        }


        private static void TestResults(DataResponse<string> results, int numberToReturn)
        {
            results.Should().Not.Be.Null();
            results.Data.Should().Not.Be.Null();
            results.Data.Count().Should().Equal(numberToReturn);
        }
    }
}
