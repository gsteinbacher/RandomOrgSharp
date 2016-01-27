using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Request
{
    [TestClass]
    public class DefaultJsonRequestBuilderTest
    {
        [TestMethod]
        public void Create_WhenCalled_ExpectNull()
        {
            // Act
            Mock<IParameters> parameters = new Mock<IParameters>();

            // Act
            var target = new DefaultJsonRequestBuilder();
            var actual = target.Build(parameters.Object);

            // Assert
            actual.Should().Be.Null();
        }
    }
}
