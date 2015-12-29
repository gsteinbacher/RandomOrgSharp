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
    public class UsageMethodTest
    {
        //[TestMethod]
        //public void WhenGetUsageCalled_ExpectNoException()
        //{
        //    // Arrange
        //    const int numberOfItems = 10;
        //    const int size = 16;
        //    const BlobFormat blobFormat = BlobFormat.Hex;

        //    Mock<IBasicMethodResponse<string>> responseMock = new Mock<IBasicMethodResponse<string>>();
        //    var expected = responseMock.Object;

        //    Mock<IBasicMethodManager<string>> basicMethodMock = new Mock<IBasicMethodManager<string>>();
        //    basicMethodMock.Setup(m => m.Generate(It.IsAny<IParameters>())).Returns(expected);

        //    var target = new UsageMethod(basicMethodMock.Object);
        //    using (new MockCommonParameters())
        //    {
        //        // Act
        //        var actual = target.GenerateBlobs(numberOfItems, size, blobFormat);

        //        // Assert
        //        actual.Should().Equal(expected);
        //    }
        //}

    }
}
