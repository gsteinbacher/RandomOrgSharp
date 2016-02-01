using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Response;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Response
{
    [TestClass]
    public class VerifyIdResponseHandlerTest
    {
        [TestMethod]
        public void Handle_WhenIdNotFoundInJson_ShouldBeZero()
        {
            // Arrange
            const int expected = 0;

            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(p => p.Id).Returns(expected);

            var input = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("notIdNode")
                );

            // Act
            VerifyIdResponseHandler target = new VerifyIdResponseHandler();
            bool actual = target.Handle(parametersMock.Object, input.ToString());

            // Assert
            actual.Should().Be.True();      // If returns true then Id is zero
        }

        [TestMethod]
        public void Handle_WhenIdIsFoundInJson_ShouldBeZero()
        {
            // Arrange
            int expected = RandomGenerator.GetInteger(1);

            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(p => p.Id).Returns(expected);

            var input = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("id", expected)
                );

            // Act
            VerifyIdResponseHandler target = new VerifyIdResponseHandler();
            bool actual = target.Handle(parametersMock.Object, input.ToString());

            // Assert
            actual.Should().Be.True();      // If returns true then Id is zero
        }

        [TestMethod, ExceptionExpected(typeof(RandomOrgRuntimeException), "does not match")]
        public void Handle_WhenIdIsNotEqualToParameterId_ShouldReturnFalse()
        {
            // Arrange
            const int expected = 12345;
            const int id = 64321;

            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(p => p.Id).Returns(expected);

            var input = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("id", id)
                );

            // Act
            VerifyIdResponseHandler target = new VerifyIdResponseHandler();
            target.Handle(parametersMock.Object, input.ToString());

            // Assert
        }

        [TestMethod]
        public void CanHandle_WhenCalled_ShouldReturnTrue()
        {
            // Arrange

            // Act
            VerifyIdResponseHandler target = new VerifyIdResponseHandler();
            var actual = target.CanHandle(null);

            // Assert
            actual.Should().Be.True();
        }
    }
}