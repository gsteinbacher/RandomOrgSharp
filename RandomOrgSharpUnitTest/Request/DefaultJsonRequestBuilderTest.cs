using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;
using Should.Fluent.Model;

namespace RandomOrgSharp.UnitTest.Request
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


        [TestMethod]
        public void CanHandle_WhenCalled_ExpectFalse()
        {
            // Act
            Mock<IParameters> parameters = new Mock<IParameters>();

            // Act
            var target = new DefaultJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(false);
        }
    }
}
