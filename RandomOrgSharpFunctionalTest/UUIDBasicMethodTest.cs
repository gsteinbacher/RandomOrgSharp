using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.BasicMethod;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class UUIDBasicMethodTest
    {
        [TestMethod]
        public void UuidBasicMethod_Execute_ShouldReturnGuidValues()
        {
            int numberToReturn = 2;

            var target = new UuidBasicMethod();
            var results = target.GenerateUuids(numberToReturn);

            results.Should().Not.Be.Null();
            results.Data.Count().Should().Equal(numberToReturn);
        }


        [TestMethod]
        public async Task UuidBasicMethod_ExecuteAsync_ShouldReturnGuidValues()
        {
            int numberToReturn = 2;

            var target = new UuidBasicMethod();
            var results = await target.GenerateUuidsAsync(numberToReturn);

            results.Should().Not.Be.Null();
            results.Data.Count().Should().Equal(numberToReturn);
        }
    }
}
