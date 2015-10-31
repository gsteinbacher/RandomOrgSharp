using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.BasicMethod;
using Obacher.RandomOrgSharp.RequestParameters;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.BasicMethod
{
    [TestClass]
    public class BasicMethodIntegerTest
    {
        [TestMethod]
        public void WhenExecuteCalled_ExpectNoException()
        {
            // Arrange
            var expected = Enumerable.Empty<int>();
            Mock<IRequestParameters> mockRequest = new Mock<IRequestParameters>();
            Mock<IBasicMethod<int>> basicMethod = new Mock<IBasicMethod<int>>();
            basicMethod.Setup(m => m.Execute(mockRequest.Object)).Returns(expected);

            // Act
            BasicMethodInteger target = new BasicMethodInteger(basicMethod.Object);
            var actual = target.Execute(mockRequest.Object);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public async Task WhenExecuteAsyncCalled_ExpectNoException()
        {
            // Arrange
            var expected = Enumerable.Empty<int>();
            Mock<IRequestParameters> mockRequest = new Mock<IRequestParameters>();
            Mock<IBasicMethod<int>> basicMethod = new Mock<IBasicMethod<int>>();
            basicMethod.Setup(m => m.ExecuteAsync(mockRequest.Object)).ReturnsAsync(expected);

            // Act
            BasicMethodInteger target = new BasicMethodInteger(basicMethod.Object);
            var actual = await target.ExecuteAsync(mockRequest.Object);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
