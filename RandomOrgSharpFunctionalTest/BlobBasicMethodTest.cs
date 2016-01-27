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
    public class BlobBasicMethodTest
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
        public void BlobBasicMethod_Execute_ShouldReteurnStringValues()
        {
            // Arrange
            const int numberToReturn = 2;
            int size = _random.Next(1, 10) * 8;

            //RandomOrgApiEmulator service = new RandomOrgApiEmulator();

            // Act
            var target = new BlobBasicMethod(_advisoryDelayHandler);
            IEnumerable<string> actual = target.GenerateBlobs(numberToReturn, size);

            // Assert
            TestResults(actual, numberToReturn);
        }


        [TestMethod]
        public async Task BlobBasicMethod_ExecuteAsync_ShouldReturnStringValues()
        {
            // Arrange
            const int numberToReturn = 2;
            int size = _random.Next(1, 10) * 8;

            //RandomOrgApiEmulator service = new RandomOrgApiEmulator();

            // Act
            var target = new BlobBasicMethod(_advisoryDelayHandler);
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
