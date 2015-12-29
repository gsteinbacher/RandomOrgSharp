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
    public class BlobBasicMethodTest
    {
        [TestMethod]
        public void WhenGenerateBlobsCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 10;
            const int size = 16;
            const BlobFormat blobFormat = BlobFormat.Hex;

            Mock<IBasicMethodResponse<string>> responseMock = new Mock<IBasicMethodResponse<string>>();
            var expected = responseMock.Object;

            Mock<IBasicMethodManager<string>> basicMethodMock = new Mock<IBasicMethodManager<string>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IParameters>())).Returns(expected);

            var target = new BlobBasicMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = target.GenerateBlobs(numberOfItems, size, blobFormat);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod]
        public async Task WhenGenerateBlobsAsyncCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 10;
            const int size = 16;
            const BlobFormat blobFormat = BlobFormat.Hex;

            Mock<IBasicMethodResponse<string>> responseMock = new Mock<IBasicMethodResponse<string>>();
            var expected = responseMock.Object;

            Mock<IBasicMethodManager<string>> basicMethodMock = new Mock<IBasicMethodManager<string>>();
            basicMethodMock.Setup(m => m.GenerateAsync(It.IsAny<IParameters>())).ReturnsAsync(expected);

            var target = new BlobBasicMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = await target.GenerateBlobsAsync(numberOfItems, size, blobFormat);

                // Assert
                actual.Should().Equal(expected);
            }
        }
    }
}
