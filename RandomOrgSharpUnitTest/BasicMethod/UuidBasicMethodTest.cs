using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.BasicMethod;
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

            Mock<IBasicMethodResponse<Guid>> responseMock = new Mock<IBasicMethodResponse<Guid>>();
            var expected = responseMock.Object;

            Mock<IBasicMethod<Guid>> basicMethodMock = new Mock<IBasicMethod<Guid>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IParameters>())).Returns(expected);

            var target = new UuidBasicMethod(basicMethodMock.Object);
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

            Mock<IBasicMethodResponse<Guid>> responseMock = new Mock<IBasicMethodResponse<Guid>>();
            var expected = responseMock.Object;

            Mock<IBasicMethod<Guid>> basicMethodMock = new Mock<IBasicMethod<Guid>>();
            basicMethodMock.Setup(m => m.GenerateAsync(It.IsAny<IParameters>())).ReturnsAsync(expected);

            var target = new UuidBasicMethod(basicMethodMock.Object);
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
