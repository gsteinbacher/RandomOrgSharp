using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Method;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;
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

            var expected = new Mock<DataResponse<int>>();
            var basicMethodMock = new Mock<IDataMethodManager<int>>();
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
