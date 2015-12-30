using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Method;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;
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

            var expected = new DataResponse<Guid>(null, Enumerable.Empty<Guid>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IDataMethodManager<Guid>> basicMethodMock = new Mock<IDataMethodManager<Guid>>();
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

            var expected = new DataResponse<Guid>(null, Enumerable.Empty<Guid>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IDataMethodManager<Guid>> basicMethodMock = new Mock<IDataMethodManager<Guid>>();
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
