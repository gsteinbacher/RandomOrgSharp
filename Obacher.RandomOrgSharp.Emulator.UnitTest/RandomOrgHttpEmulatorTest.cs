using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.Method;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.Emulator.UnitTest
{
    [TestClass]
    public class RandomOrgHttpEmulatorTest
    {
        [TestMethod]
        public void SendRequest_WhenIntegerCalledWithBase10_ExpectBase10IntegerValuesReturned()
        {
            // Arrange
            const int numberOfItemsReturned = 100;
            const int minValue = 1;
            const int maxValue = 1000000;

            var expected = new Mock<DataResponseInfo<int>>();
            var basicMethodMock = new Mock<IMethodCallBroker<int>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IntegerParameters>())).Returns(expected.Object);

            // Act
            var target = new IntegerMethod(basicMethodMock.Object);
            var actual = target.GenerateIntegers(numberOfItemsReturned, minValue, maxValue);

            // Assert
            actual.Should().Not.Be.Null();
            actual.Should().Equal(expected);
        }
    }
}
