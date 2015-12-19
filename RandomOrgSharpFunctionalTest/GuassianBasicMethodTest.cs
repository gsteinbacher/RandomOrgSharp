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
    public class GuassianBasicMethodTest
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
        public void GuassianBasicMethodTest_Execute_ShouldReturnDoubleValuesInRange()
        {
            int numberToReturn = _random.Next(5, 20);
            int mean = _random.Next(-1000000, 1000000);
            int standardDeviation = _random.Next(-1000000, 1000000);
            int signifantDigits = _random.Next(2, 20);
            IRandomOrgService service = new RandomOrgApiService();

            var target = new GuassianBasicMethod(service, _manager);

            IRequestParameters requestParameters = new GuassianRequestParameters(numberToReturn, mean, standardDeviation, signifantDigits);
            var results = target.Execute(requestParameters);

            TestResults(results, numberToReturn, mean);
        }

        [TestMethod]
        public async Task GuassianBasicMethod_ExecuteAsync_ShouldReturnDoubleValuesInRange()
        {
            int numberToReturn = _random.Next(5, 20);
            int mean = _random.Next(-1000000, 1000000);
            int standardDeviation = _random.Next(-1000000, 1000000);
            int signifantDigits = _random.Next(2, 20);

            IRandomOrgService service = new RandomOrgApiService();

            var target = new GuassianBasicMethod(service, _manager);

            IRequestParameters requestParameters = new GuassianRequestParameters(numberToReturn, mean, standardDeviation, signifantDigits);
            var results = await target.ExecuteAsync(requestParameters);

            TestResults(results, numberToReturn, mean);
        }


        private static void TestResults(IEnumerable<decimal> results, int numberToReturn, int numberOfDecimalPlaces)
        {
            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);
        }
    }
}
