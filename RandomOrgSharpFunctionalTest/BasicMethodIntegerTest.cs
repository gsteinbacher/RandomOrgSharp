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
    public class BasicMethodIntegerTest
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
        public void BasicMethodInteger_Execute_ShouldReturnIntegerValuesInRange()
        {
            int numberToReturn = _random.Next(5, 20);
            int minNumber = _random.Next(1, 1000);
            int maxNumber = _random.Next(minNumber + 1, 1000000);
            const bool allowDuplicates = false;
            const BaseNumberOptions baseNumber = BaseNumberOptions.Ten;

            IRandomOrgService service = new RandomOrgApiService();

            BasicMethodInteger target = new BasicMethodInteger(service, _manager);

            IRequestParameters requestParameters = new IntegerRequestParameters(numberToReturn, minNumber, maxNumber, allowDuplicates, baseNumber);
            var results = target.Execute(requestParameters);

            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);

            foreach (var result in results)
                result.Should().Be.InRange(minNumber, maxNumber);
        }


        [TestMethod]
        public async Task BasicMethodInteger_ExecuteAsync_ShouldReturnIntegerValuesInRange()
        {
            int numberToReturn = _random.Next(5, 20);
            int minNumber = _random.Next(1, 1000);
            int maxNumber = _random.Next(minNumber + 1, 1000000);
            const bool allowDuplicates = false;
            const BaseNumberOptions baseNumber = BaseNumberOptions.Ten;

            IRandomOrgService service = new RandomOrgApiService();

            BasicMethodInteger target = new BasicMethodInteger(service, _manager);

            IRequestParameters requestParameters = new IntegerRequestParameters(numberToReturn, minNumber, maxNumber, allowDuplicates, baseNumber);
            var results = await target.ExecuteAsync(requestParameters);

            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);

            foreach (var result in results)
                result.Should().Be.InRange(minNumber, maxNumber);
        }
    }
}
