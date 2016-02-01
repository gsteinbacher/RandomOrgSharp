using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Method;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class StringBasicMethodTest
    {
        [TestMethod]
        public void StringBasicMethod_Execute_ShouldReteurnStringValues()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();
            int numberToReturn = 2;
            int length = bmt.Random.Next(1, 20);
            CharactersAllowed charactersAllowed = CharactersAllowed.AlphaNumeric;
            const bool allowDuplicates = false;

            // Act
            var target = new StringBasicMethod(bmt.AdvisoryDelayHandler, bmt.Service);
            var results = target.GenerateStrings(numberToReturn, length, charactersAllowed, allowDuplicates);

            // Assert
            TestResults(results.ToList(), numberToReturn, length,
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
        }


        [TestMethod]
        public async Task StringBasicMethod_ExecuteAsync_ShouldReturnStringValues()
        {
            // Arrange
            BaseMethodTest bmt = new BaseMethodTest();
            int numberToReturn = 2;
            int length = bmt.Random.Next(1, 20);
            CharactersAllowed charactersAllowed = CharactersAllowed.AlphaNumeric;
            const bool allowDuplicates = false;

            var target = new StringBasicMethod(bmt.AdvisoryDelayHandler, bmt.Service);
            var results = await target.GenerateStringsAsync(numberToReturn, length, charactersAllowed, allowDuplicates);

            // Assert
            TestResults(results.ToList(), numberToReturn, length,
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
        }


        private static void TestResults(IList<string> results, int numberToReturn, int length,
            string charactersAllowed)
        {
            results.Should().Not.Be.Null();
            results.Count.Should().Equal(numberToReturn);

            foreach (var result in results)
            {
                result.Length.Should().Be.LessThanOrEqualTo(length);
                result.ToCharArray().Should().Contain.Any(charactersAllowed.Contains);
            }
        }
    }
}
