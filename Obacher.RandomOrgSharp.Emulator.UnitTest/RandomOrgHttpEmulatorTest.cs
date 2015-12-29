using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.BasicMethod;
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

            var basicMethodResponseMock = new Mock<IBasicMethodResponse<int>>();
            var basicMethodMock = new Mock<IBasicMethodManager<int>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IntegerParameters>())).Returns(basicMethodResponseMock.Object);

            // Act
            var target = new IntegerBasicMethod(basicMethodMock.Object);
            var response = target.GenerateIntegers(numberOfItemsReturned, minValue, maxValue);

            // Assert
            response.Should().Not.Be.Null();
            response.Should().Equal(basicMethodResponseMock.Object);
        }
    }
}
