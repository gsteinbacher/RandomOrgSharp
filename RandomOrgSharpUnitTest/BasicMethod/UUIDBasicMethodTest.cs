using System;
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
    public class UUIDBasicMethodTest
    {
        [TestMethod]
        public void WhenExecuteCalled_ExpectNoException()
        {
            // Arrange
            var parameters = Enumerable.Empty<string>();
            var expected = Array.ConvertAll(parameters.ToArray(), id => new Guid(id));

            Mock<IRequestParameters> mockRequest = new Mock<IRequestParameters>();
            Mock<IBasicMethod<string>> basicMethod = new Mock<IBasicMethod<string>>();
            basicMethod.Setup(m => m.Generate(mockRequest.Object)).Returns(parameters);

            // Act
            var target = new UUIDBasicMethod(basicMethod.Object);
            var actual = target.Execute(mockRequest.Object);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public async Task WhenExecuteAsyncCalled_ExpectNoException()
        {
            // Arrange
            var parameters = Enumerable.Empty<string>();
            var expected = Array.ConvertAll(parameters.ToArray(), id => new Guid(id));
            Mock<IRequestParameters> mockRequest = new Mock<IRequestParameters>();
            Mock<IBasicMethod<string>> basicMethod = new Mock<IBasicMethod<string>>();
            basicMethod.Setup(m => m.GenerateAsync(mockRequest.Object)).ReturnsAsync(parameters);

            // Act
            var target = new UUIDBasicMethod(basicMethod.Object);
            var actual = await target.ExecuteAsync(mockRequest.Object);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
