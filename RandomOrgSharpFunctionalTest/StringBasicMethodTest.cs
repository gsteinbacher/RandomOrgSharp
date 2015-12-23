using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.BasicMethod;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class StringBasicMethodTest
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
        public void StringBasicMethod_Execute_ShouldReteurnStringValues()
        {
            int numberToReturn = _random.Next(5, 20);
            int length = _random.Next(1, 20);
            CharactersAllowed charactersAllowed = CharactersAllowed.AlphaNumeric;
            const bool allowDuplicates = false;

            IRandomOrgService service = new RandomOrgApiService();

            var target = new StringBasicMethod(service, _manager);

            IRequestParameters requestParameters = new StringRequestParameters(numberToReturn, length, charactersAllowed,
                allowDuplicates);
            var results = target.Execute(requestParameters);

            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);

            TestResults(results, numberToReturn, length,
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
        }


        [TestMethod]
        public async Task StringBasicMethod_ExecuteAsync_ShouldReturnStringValues()
        {
            int numberToReturn = _random.Next(5, 20);
            int length = _random.Next(1, 20);
            CharactersAllowed charactersAllowed = CharactersAllowed.AlphaNumeric;
            const bool allowDuplicates = false;

            IRandomOrgService service = new RandomOrgApiService();

            var target = new StringBasicMethod(service, _manager);

            IRequestParameters requestParameters = new StringRequestParameters(numberToReturn, length, charactersAllowed,
                allowDuplicates);
            var results = await target.ExecuteAsync(requestParameters);

            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);

            TestResults(results, numberToReturn, length,
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
        }


        private static void TestResults(IEnumerable<string> results, int numberToReturn, int length,
            string charactersAllowed)
        {
            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);

            foreach (var result in results)
            {
                result.Length.Should().Be.LessThanOrEqualTo(length);
                result.ToCharArray().Should().Contain.Any(charactersAllowed.Contains);
            }
        }
    }
}
