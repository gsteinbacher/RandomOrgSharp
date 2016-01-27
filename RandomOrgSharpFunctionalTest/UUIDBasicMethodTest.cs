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
    public class UuidBasicMethodTest
    {
        private static AdvisoryDelayHandler _advisoryDelayHandler;

        [ClassInitialize]
        public static void InitializeTests(TestContext context)
        {
            _advisoryDelayHandler = new AdvisoryDelayHandler(new DateTimeWrap());
        }

        [ClassCleanup]
        public static void CleanupTest()
        {
            _advisoryDelayHandler = null;
        }

        [TestMethod]
        public void UuidBasicMethod_Execute_ShouldReturnGuidValues()
        {
            int numberToReturn = 2;

            var target = new UuidBasicMethod(_advisoryDelayHandler);
            IEnumerable<Guid> results = target.GenerateUuids(numberToReturn);

            results.Should().Not.Be.Null();
            results.Count().Should().Equal(numberToReturn);
        }


        [TestMethod]
        public async Task UuidBasicMethod_ExecuteAsync_ShouldReturnGuidValues()
        {
            int numberToReturn = 2;

            var target = new UuidBasicMethod(_advisoryDelayHandler);
            IEnumerable<Guid> results = await target.GenerateUuidsAsync(numberToReturn);

            results.Should().Not.Be.Null();
            results.Count().Should().Equal(numberToReturn);
        }
    }
}
