using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.BasicMethod;
using Obacher.RandomOrgSharp.RequestParameters;
using Should.Fluent;

namespace RandomOrgSharpFunctionalTest
{
    [TestClass]
    public class BasicMethodIntegerTest
    {
        private readonly Random _random;

        public BasicMethodIntegerTest()
        {
            _random = new Random();
        }

        [TestMethod]
        public void BasicMethodInteger_Execute_ShouldReturnIntegerValuesInRange()
        {
            int numberToReturn = _random.Next(100, 1000);
            int minNumber = _random.Next(1, 1000);
            int maxNumber = _random.Next(minNumber + 1, 1000000);
            const bool allowDuplicates = false;
            const BaseNumberOptions baseNumber = BaseNumberOptions.Ten;

            IRandomOrgService service = new RandomOrgApiService();

            BasicMethodInteger target = new BasicMethodInteger(service);

            IRequestParameters requestParameters = new IntegerRequestParameters(numberToReturn, minNumber, maxNumber, allowDuplicates, baseNumber);
            var results = target.Execute(requestParameters);

            results.Should().Not.Be.Null();
            results.Should().Not.Be.Empty();
            results.Count().Should().Equal(numberToReturn);

            foreach (var result in results)
                result.Should().Be.InRange(minNumber, maxNumber);
        }
    }
}
