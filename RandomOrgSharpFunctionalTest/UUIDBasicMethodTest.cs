using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.BasicMethod;
using Obacher.RandomOrgSharp.RequestParameters;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class UUIDBasicMethodTest
    {
        private Random _random;
        private IMethodCallManager _manager;

        [TestInitialize]
        public void InitializeTests()
        {
            _manager = new MethodCallManager();
            _random = new Random();

        }

        [TestMethod]
        public void UUIDBasicMethod_Execute_ShouldReturnGuidValues()
        {
            int numberToReturn = _random.Next(5, 20);

            IRandomOrgService service = new RandomOrgApiService();

            var target = new UUIDBasicMethod(service, _manager);

            IRequestParameters requestParameters = new UUIDRequestParameters(numberToReturn);
            var results = target.Execute(requestParameters);

            results.Should().Not.Be.Null();
            results.Count().Should().Equal(numberToReturn);
        }


        [TestMethod]
        public async Task UUIDBasicMethod_ExecuteAsync_ShouldReturnGuidValues()
        {
            int numberToReturn = _random.Next(5, 20);

            IRandomOrgService service = new RandomOrgApiService();

            var target = new UUIDBasicMethod(service, _manager);

            IRequestParameters requestParameters = new UUIDRequestParameters(numberToReturn);
            var results = await target.ExecuteAsync(requestParameters);

            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);
        }
    }
}
