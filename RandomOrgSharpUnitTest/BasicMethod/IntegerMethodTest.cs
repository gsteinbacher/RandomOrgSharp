using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.Method;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.BasicMethod
{
    [TestClass]
    public class IntegerMethodTest
    {
        [TestMethod]
        public void WhenGenerateIntegersCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = 1000;
            const bool allowDuplicates = false;

            var expected = new DataResponseInfo<int>(null, Enumerable.Empty<int>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IMethodCallBroker<int>> basicMethodMock = new Mock<IMethodCallBroker<int>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IParameters>())).Returns(expected);

            var target = new IntegerMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = target.GenerateIntegers(numberOfItems, minimumValue, maximumValue, allowDuplicates);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod]
        public async Task WhenGenerateGuassiansAsyncCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int minimumValue = 10;
            const int maximumValue = 1000;
            const bool allowDuplicates = false;

            var expected = new DataResponseInfo<int>(null, Enumerable.Empty<int>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IMethodCallBroker<int>> basicMethodMock = new Mock<IMethodCallBroker<int>>();
            basicMethodMock.Setup(m => m.GenerateAsync(It.IsAny<IParameters>())).ReturnsAsync(expected);

            var target = new IntegerMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = await target.GenerateIntegersAsync(numberOfItems, minimumValue, maximumValue, allowDuplicates);

                // Assert
                actual.Should().Equal(expected);
            }
        }
    }
}
