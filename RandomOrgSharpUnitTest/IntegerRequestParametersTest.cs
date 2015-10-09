using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.RequestParameters;
using Should.Fluent;

namespace RandomOrgSharpUnitTest
{
    [TestClass]
    public class IntegerRequestParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            const int numberOfItems = -1;
            const int minimumValue = 10;
            const int maximumValue = 1000;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 10000;
            const int minimumValue = int.MinValue;
            const int maximumValue = 1000;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMinimumNumberLessThenMinimumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int minimumValue = int.MinValue;
            const int maximumValue = 1000;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMinimumNumberGreaterThenMaximumllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int minimumValue = int.MaxValue;
            const int maximumValue = 1000;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMaxmimumNumberLessThenMinimumAllowed_ExpectException()
        {
            const int numberOfItems = 10000;
            const int minimumValue = 10;
            const int maximumValue = int.MinValue;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenMaxmimumNumberGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = int.MaxValue;

            new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
        }


        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = 1000;
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
                    new JProperty(RandomOrgConstants.METHOD_PARAMETER_NAME, "generateIntegers"),
                    new JProperty(RandomOrgConstants.PARAMETERS_PARAMETER_NAME,
                        new JObject(
                            new JProperty(RandomOrgConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                            new JProperty(RandomOrgConstants.MINIMUM_VALUE_PARAMETER_NAME, minimumValue),
                            new JProperty(RandomOrgConstants.MAXIMUM_VALUE_PARAMETER_NAME, maximumValue),
                            new JProperty(RandomOrgConstants.REPLACEMENT_PARAMETER_NAME, true),
                            new JProperty(RandomOrgConstants.BASE_NUMBER_PARAMETER_NAME, 10),
                           new JProperty(RandomOrgConstants.APIKEY_KEY, RandomOrgConstants.APIKEY_VALUE))),
                        new JProperty(RandomOrgConstants.ID_PARAMETER_NAME, 999));


            var random = new Mock<IRandom>();
            random.Setup(m => m.Next()).Returns(id);
            RandomNumberGenerator.Instance = random.Object;

            var target = new IntegerRequestParameters(numberOfItems, minimumValue, maximumValue);
            var actual = target.CreateJsonRequest();

            actual.Should().Equal(expected);
        }
    }
}
