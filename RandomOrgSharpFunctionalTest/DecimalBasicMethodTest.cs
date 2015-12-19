using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class DecimalBasicMethodTest
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
        public void DecimalBasicMethod_Execute_ShouldReturnDecimalValuesInRange()
        {
            int numberToReturn = _random.Next(5, 20);
            int numberOfDecimalPlaces = _random.Next(1, 20);
            const bool allowDuplicates = false;

            IRandomOrgService service = new RandomOrgApiService();

            var target = new DecimalBasicMethod(service, _manager);

            IRequestParameters requestParameters = new DecimalRequestParameters(numberToReturn, numberOfDecimalPlaces, allowDuplicates);
            var results = target.Execute(requestParameters);

            TestResults(results, numberToReturn, numberOfDecimalPlaces);
        }

        [TestMethod]
        public async Task DecimalBasicMethod_ExecuteAsync_ShouldReturnDecimalValuesInRange()
        {
            int numberToReturn = _random.Next(5, 20);
            int numberOfDecimalPlaces = _random.Next(1, 20);
            const bool allowDuplicates = false;

            IRandomOrgService service = new RandomOrgApiService();

            var target = new DecimalBasicMethod(service, _manager);

            IRequestParameters requestParameters = new DecimalRequestParameters(numberToReturn, numberOfDecimalPlaces, allowDuplicates);
            var results = await target.ExecuteAsync(requestParameters);

            TestResults(results, numberToReturn, numberOfDecimalPlaces);
        }


        private static void TestResults(IEnumerable<decimal> results, int numberToReturn, int numberOfDecimalPlaces)
        {
            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);

            foreach (var result in results)
            {
                result.Should().Be.GreaterThan(0m);
                result.Should().Be.LessThan(1m);
                result.ToString(CultureInfo.InvariantCulture).Length.Should().Be.LessThanOrEqualTo(numberOfDecimalPlaces + 2);  // the "+2" takes into account the "0." at the beginning of returned value
            }
        }
    }
}
