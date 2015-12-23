using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.BasicMethod;
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
            int numberOfItemsReturned = 100;
            int minValue = 1;
            int maxValue = 1000000;
            RandomOrgServiceEmulator emulator = new RandomOrgServiceEmulator();
            MethodCallManager callManager = new MethodCallManager();

            // Act
            IntegerRequestParameters parameters = new IntegerRequestParameters(numberOfItemsReturned, minValue, maxValue);
            IntegerBasicMethod target = new IntegerBasicMethod(emulator, callManager);
            var response = target.Execute(parameters);

            // Assert
            response.Should().Not.Be.Null();
            response.Count().Should().Equal(numberOfItemsReturned);

            foreach (int value in response)
                value.Should().Be.InRange(minValue, maxValue);
        }
    }
}
