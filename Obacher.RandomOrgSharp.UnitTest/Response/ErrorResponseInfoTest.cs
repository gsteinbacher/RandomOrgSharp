using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Response
{
    [TestClass]
    public class ErrorResponseInfoTest
    {
        [TestMethod]
        public void Constructor_WhenCalled_ExpectAllValuesSet()
        {
            const string version = "2.0";
            const int code = 999;
            const string message = "Message";
            const int id = 400;

            ErrorResponseInfo target = new ErrorResponseInfo(version, id, code, message);

            target.Version.Should().Equal(version);
            target.Code.Should().Equal(code);
            target.Message.Should().Equal(message);
            target.AdvisoryDelay.Should().Equal(0);
            target.Id.Should().Equal(id);
        }

        [TestMethod]
        public void IsEmpty_WhenEmpty_ShouldReturnTrue()
        {
            // Arrange

            // Act
            ErrorResponseInfo target = ErrorResponseInfo.Empty();

            // Assert
            target.IsEmpty().Should().Be.True();
        }

        [TestMethod]
        public void IsEmpty_WhenMessageIsNull_ShouldReturnTrue()
        {
            // Arrange

            // Act
            ErrorResponseInfo target = new ErrorResponseInfo(string.Empty, 0, 0, null);

            // Assert
            target.IsEmpty().Should().Be.True();
        }

        [TestMethod]
        public void IsEmpty_WhenMessageIsEmpty_ShouldReturnTrue()
        {
            // Arrange

            // Act
            ErrorResponseInfo target = new ErrorResponseInfo(string.Empty, 0, 0, string.Empty);

            // Assert
            target.IsEmpty().Should().Be.True();
        }

        [TestMethod]
        public void IsEmpty_WhenMessageIsWhiteSpace_ShouldReturnTrue()
        {
            // Arrange

            // Act
            ErrorResponseInfo target = new ErrorResponseInfo(string.Empty, 0, 0, "       ");

            // Assert
            target.IsEmpty().Should().Be.True();
        }

        [TestMethod]
        public void IsEmpty_WhenCodeIsNotZero_ShouldReturnFalse()
        {
            // Arrange

            // Act
            ErrorResponseInfo target = new ErrorResponseInfo(string.Empty, 0, 400, string.Empty);

            // Assert
            target.IsEmpty().Should().Be.False();
        }

        [TestMethod]
        public void IsEmpty_WhenMessageIsNotNullOrEmpty_ShouldReturnFalse()
        {
            // Arrange

            // Act
            ErrorResponseInfo target = new ErrorResponseInfo(string.Empty, 0, 0, "Test Message");

            // Assert
            target.IsEmpty().Should().Be.False();
        }

        [TestMethod]
        public void IsEmpty_WhenCodeAndMessageAreNotEmpty_ShouldReturnFalse()
        {
            // Arrange

            // Act
            ErrorResponseInfo target = new ErrorResponseInfo(string.Empty, 0, 400, "Test Message");

            // Assert
            target.IsEmpty().Should().Be.False();
        }

        [TestMethod]
        public void Equals_WhenSameReference_ShouldReturnTrue()
        {
            // Arrange
            var one = ErrorResponseInfo.Empty();
            var two = one;

            // Act
            var actual = one.Equals((object)two);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void Equals_WhenComparedValueIsNull_ShouldReturnFalse()
        {
            // Arrange
            var one = ErrorResponseInfo.Empty();

            // Act
            var actual = one.Equals((object)null);

            // Assert
            actual.Should().Be.False();
        }

        [TestMethod]
        public void Equals_WhenTypesAreDifferent_ShouldReturnFalse()
        {
            // Arrange
            var one = ErrorResponseInfo.Empty();
            Mock<IResponseInfo> responseInfoMock = new Mock<IResponseInfo>();

            // Act
            var actual = one.Equals(responseInfoMock.Object);

            // Assert
            actual.Should().Be.False();
        }

        [TestMethod]
        public void Equals_WhenPropertiesMatch_ShouldReturnTrue()
        {
            // Arrange
            const string version = "2.0";
            int id = RandomGenerator.GetInteger(1);
            const int code = 400;
            const string message = "Test Message";

            var one = new ErrorResponseInfo(version, id, code, message);
            var two = new ErrorResponseInfo(version, id, code, message);

            // Act
            var actual = one.Equals((object)two);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void Equals_WhenPropertiesDoNotMatch_ShouldReturnFalse()
        {
            // Arrange
            const string version = "2.0";
            int id = RandomGenerator.GetInteger(1);
            const int code = 400;
            const string message = "Test Message";

            var one = new ErrorResponseInfo(version, id, code, message);
            var two = new ErrorResponseInfo(version, id, 12346, message);

            // Act
            var actual = one.Equals(two);

            // Assert
            actual.Should().Be.False();
        }

        [TestMethod]
        public void GetHashCode_WhenCalled_ShouldNotThrowException()
        {
            // Arrange
            const string version = "2.0";
            int id = RandomGenerator.GetInteger(1);
            const int code = 400;
            const string message = "Test Message";

            var target = new ErrorResponseInfo(version, id, code, message);

            // Act
            var actual = target.GetHashCode();

            // Assert
            actual.Should().Not.Equal(0);
        }
    }
}
