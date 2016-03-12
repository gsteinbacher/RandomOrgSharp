using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Method;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [TestClass]
    public class UsageMethodTest
    {
        [TestMethod]
        public void UsageMethod_Execute_ShouldReturnValidUsageResponse()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();

            // Act
            var target = new UsageMethod(bmt.Service);
            var results = target.GetUsage();

            // Assert
            TestResults(results);
        }


        [TestMethod]
        public async Task UsageMethodAsync_Execute_ShouldReturnValidUsageResponse()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();

            // Act
            var target = new UsageMethod(bmt.Service);
            var results = await target.GetUsageAsync();

            // Assert
            TestResults(results);
        }


        private static void TestResults(UsageResponseInfo results)
        {
            results.Should().Not.Be.Null();
            results.Version.Should().Not.Be.Null();
            results.AdvisoryDelay.Should().Equal(0);
            results.Id.Should().Be.GreaterThan(0);
            results.CreationTime.Date.Should().Be.LessThan(DateTime.Today);
            results.Status.Should().Equal(StatusType.Running);
        }
    }
}
