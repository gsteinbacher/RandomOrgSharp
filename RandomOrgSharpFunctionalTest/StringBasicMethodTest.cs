using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Method;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class StringBasicMethodTest
    {
        private Random _random;

        [TestInitialize]
        public void InitializeTests()
        {
            _random = new Random();
        }

        [TestMethod]
        public void StringBasicMethod_Execute_ShouldReteurnStringValues()
        {
            // Assert
            int numberToReturn = 2;
            int length = _random.Next(1, 20);
            CharactersAllowed charactersAllowed = CharactersAllowed.AlphaNumeric;
            const bool allowDuplicates = false;

            // Act
            var target = new StringMethod();
            var results = target.GenerateStrings(numberToReturn, length, charactersAllowed, allowDuplicates);

            TestResults(results, numberToReturn, length,
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
        }


        [TestMethod]
        public async Task StringBasicMethod_ExecuteAsync_ShouldReturnStringValues()
        {
            int numberToReturn = 2;
            int length = _random.Next(1, 20);
            CharactersAllowed charactersAllowed = CharactersAllowed.AlphaNumeric;
            const bool allowDuplicates = false;

            var target = new StringMethod();
            var results = await target.GenerateStringsAsync(numberToReturn, length, charactersAllowed, allowDuplicates);

            TestResults(results, numberToReturn, length,
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
        }


        private static void TestResults(DataResponse<string> results, int numberToReturn, int length,
            string charactersAllowed)
        {
            results.Should().Not.Be.Null();
            results.Data.Count().Should().Equal(numberToReturn);

            foreach (var result in results.Data)
            {
                result.Length.Should().Be.LessThanOrEqualTo(length);
                result.ToCharArray().Should().Contain.Any(charactersAllowed.Contains);
            }
        }
    }
}
