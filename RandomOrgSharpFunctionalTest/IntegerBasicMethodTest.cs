using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.BasicMethod;
using Obacher.RandomOrgSharp.Response;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class IntegerBasicMethodTest
    {
        private Random _random;

        [TestInitialize]
        public void InitializeTests()
        {
            _random = new Random();
        }

        [TestMethod]
        public void IntegerBasicMethod_Execute_ShouldReturnIntegerValuesInRange()
        {
            // Arrange
            int numberToReturn = 2;
            int minNumber = _random.Next(1, 1000);
            int maxNumber = _random.Next(minNumber + 1, 1000000);
            bool allowDuplicates = _random.Next(1, 2) == 1;

            // Act
            var target = new IntegerBasicMethod();
            var results = target.GenerateIntegers(numberToReturn, minNumber, maxNumber, allowDuplicates);

            // Assert
            TestResults(results, numberToReturn, minNumber, maxNumber, allowDuplicates);
        }


        [TestMethod]
        public async Task IntegerBasicMethod_ExecuteAsync_ShouldReturnIntegerValuesInRange()
        {
            int numberToReturn = 2;
            int minNumber = _random.Next(1, 1000);
            int maxNumber = _random.Next(minNumber + 1, 1000000);
            bool allowDuplicates = _random.Next(1, 2) == 1;

            // Act
            var target = new IntegerBasicMethod();
            var results = await target.GenerateIntegersAsync(numberToReturn, minNumber, maxNumber, allowDuplicates);

            TestResults(results, numberToReturn, minNumber, maxNumber, allowDuplicates);
        }

        private static void TestResults(IBasicMethodResponse<int> results, int numberToReturn, int minNumber,
                                        int maxNumber, bool allowDuplicates)
        {
            results.Should().Not.Be.Null();
            results.Data.Count().Should().Equal(numberToReturn);

            // Ensure there are no duplicates
            if (!allowDuplicates)
                CollectionAssert.AllItemsAreUnique(results.Data.ToArray());

            foreach (var result in results.Data)
                result.Should().Be.InRange(minNumber, maxNumber);
        }
    }
}