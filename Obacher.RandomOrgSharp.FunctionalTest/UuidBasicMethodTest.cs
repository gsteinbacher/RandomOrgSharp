using System;
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
    public class UuidBasicMethodTest
    {
        [TestMethod]
        public void UuidBasicMethod_Execute_ShouldReturnGuidValues()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();
            int numberToReturn = 2;

            // Act
            var target = new UuidBasicMethod(bmt.AdvisoryDelayHandler, bmt.Service);
            IEnumerable<Guid> results = target.GenerateUuids(numberToReturn);

            // Assert
            results.Should().Not.Be.Null();
            results.Count().Should().Equal(numberToReturn);
        }


        [TestMethod]
        public async Task UuidBasicMethod_ExecuteAsync_ShouldReturnGuidValues()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();
            int numberToReturn = 2;

            // Act
            var target = new UuidBasicMethod(bmt.AdvisoryDelayHandler, bmt.Service);
            IEnumerable<Guid> results = await target.GenerateUuidsAsync(numberToReturn);

            // Assert
            results.Should().Not.Be.Null();
            results.Count().Should().Equal(numberToReturn);
        }
    }
}
