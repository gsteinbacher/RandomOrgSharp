using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.BasicMethod;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class BlobBasicMethodTest
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
        public void BlobBasicMethod_Execute_ShouldReteurnStringValues()
        {
            int numberToReturn = _random.Next(5, 20);
            int size = _random.Next(1, 1000) * 8;
            BlobFormat format = BlobFormat.Base64;


            BlobRequestParameters requestParameters = new BlobRequestParameters(numberToReturn, size, format);
            IParameterBuilder parameterBuilder = new BlobJsonParameterBuilder(requestParameters);
            IJsonRequestBuilder requestBuilder = new JsonRequestBuilder(parameterBuilder);

            IRandomOrgService service = new RandomOrgApiService();
            var target = new BlobBasicMethod(service, _manager);
            var results = target.Execute(requestBuilder);

            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);

            TestResults(results, numberToReturn, size);
        }


        [TestMethod]
        public async Task BlobBasicMethod_ExecuteAsync_ShouldReturnStringValues()
        {
            int numberToReturn = _random.Next(5, 20);
            int size = _random.Next(8, 1000) * 8;
            BlobFormat format = BlobFormat.Base64;

            IRandomOrgService service = new RandomOrgApiService();

            var target = new StringBasicMethod(service, _manager);

            IRequestParameters requestParameters = new BlobRequestParameters(numberToReturn, size, format);
            var results = await target.ExecuteAsync(requestParameters);

            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);

            TestResults(results, numberToReturn, size);
        }


        private static void TestResults(IEnumerable<string> results, int numberToReturn, int size)
        {
            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);

            foreach (var result in results)
                result.Length.Should().Equal(size);
        }
    }
}
