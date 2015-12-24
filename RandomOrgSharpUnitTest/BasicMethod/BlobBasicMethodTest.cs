using System.Linq;
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
        [TestMethod, Ignore]
        public void WhenGenerateBlobsCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 10;
            const int size = 11;
            BlobFormat blobFormat = BlobFormat.Base64;

            ConfigMocks.SetupApiKeyMock();
            ConfigMocks.SetupIdMock();

            Mock<IBasicMethodResponse<string>> responseMock = new Mock<IBasicMethodResponse<string>>();
            var expected = responseMock.Object;

            Mock<IBasicMethod<string>> basicMethodMock = new Mock<IBasicMethod<string>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IParameters>())).Returns(expected);

            // Act
            var target = new BlobBasicMethod(basicMethodMock.Object);
            var actual = target.GenerateBlobs(numberOfItems, size, blobFormat);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod, Ignore]
        public async Task WhenGenerateBlobsAsyncCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 10;
            const int size = 11;
            const BlobFormat blobFormat = BlobFormat.Base64;

            ConfigMocks.SetupApiKeyMock();
            ConfigMocks.SetupIdMock();

            Mock<IBasicMethodResponse<string>> responseMock = new Mock<IBasicMethodResponse<string>>();
            var expected = responseMock.Object;

            Mock<IBasicMethod<string>> basicMethodMock = new Mock<IBasicMethod<string>>();
            basicMethodMock.Setup(m => m.GenerateAsync(It.IsAny<IParameters>())).ReturnsAsync(expected);

            // Act
            var target = new BlobBasicMethod(basicMethodMock.Object);
            var actual = await target.GenerateBlobsAsync(numberOfItems, size, blobFormat);

            // Assert
            actual.Should().Equal(expected);

        }
    }
}
