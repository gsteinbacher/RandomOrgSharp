using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.Method;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.BasicMethod
{
    [TestClass]
    public class UuidBasicMethodTest
    {
        [TestMethod]
        public void WhenGenerateUuidsCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 1;

            var expected = new DataResponseInfo<Guid>(null, Enumerable.Empty<Guid>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IMethodCallBroker<Guid>> basicMethodMock = new Mock<IMethodCallBroker<Guid>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IParameters>())).Returns(expected);

            var target = new UuidMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = target.GenerateUuids(numberOfItems);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod]
        public async Task WhenGenerateUuidsAsyncCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 1;

            var expected = new DataResponseInfo<Guid>(null, Enumerable.Empty<Guid>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IMethodCallBroker<Guid>> basicMethodMock = new Mock<IMethodCallBroker<Guid>>();
            basicMethodMock.Setup(m => m.GenerateAsync(It.IsAny<IParameters>())).ReturnsAsync(expected);

            var target = new UuidMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = await target.GenerateUuidsAsync(numberOfItems);

                // Assert
                actual.Should().Equal(expected);
            }
        }
    }
}
