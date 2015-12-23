using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.BasicMethod;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.BasicMethod
{
    [TestClass]
    public class GuassianBasicMethodTest
    {
        [TestMethod]
        public void WhenExecuteCalled_ExpectNoException()
        {
            // Arrange
            var expected = Enumerable.Empty<decimal>();
            Mock<IRequestParameters> mockRequest = new Mock<IRequestParameters>();
            Mock<IBasicMethod<decimal>> basicMethod = new Mock<IBasicMethod<decimal>>();
            basicMethod.Setup(m => m.Generate(mockRequest.Object)).Returns(expected);

            // Act
            var target = new GuassianBasicMethod(basicMethod.Object);
            var actual = target.Execute(mockRequest.Object);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public async Task WhenExecuteAsyncCalled_ExpectNoException()
        {
            // Arrange
            var expected = Enumerable.Empty<decimal>();
            Mock<IRequestParameters> mockRequest = new Mock<IRequestParameters>();
            Mock<IBasicMethod<decimal>> basicMethod = new Mock<IBasicMethod<decimal>>();
            basicMethod.Setup(m => m.GenerateAsync(mockRequest.Object)).ReturnsAsync(expected);

            // Act
            var target = new GuassianBasicMethod(basicMethod.Object);
            var actual = await target.ExecuteAsync(mockRequest.Object);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
