using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.JsonRpc;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.Method;
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

            var expected = new DataResponseInfo<string>(null, Enumerable.Empty<string>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IMethodCallBroker<string>> basicMethodMock = new Mock<IMethodCallBroker<string>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IParameters>())).Returns(expected);

            var target = new BlobMethod(basicMethodMock.Object);
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

            var expected = new DataResponseInfo<string>(null, Enumerable.Empty<string>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IMethodCallBroker<string>> basicMethodMock = new Mock<IMethodCallBroker<string>>();
            basicMethodMock.Setup(m => m.GenerateAsync(It.IsAny<IParameters>())).ReturnsAsync(expected);

            var target = new BlobMethod(basicMethodMock.Object);
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
