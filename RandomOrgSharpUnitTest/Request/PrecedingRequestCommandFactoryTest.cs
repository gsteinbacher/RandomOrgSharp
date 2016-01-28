using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Request
{
    [TestClass]
    public class PrecedingRequestCommandFactoryTest
    {
        [TestMethod]
        public void Execute_WhenNoCommands_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            // Act
            PrecedingRequestCommandFactory target = new PrecedingRequestCommandFactory();
            var actual = target.Execute(parameters.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void Execute_WhenCanHandleReturnsFalse_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestCommand> requestBuilderMock = new Mock<IRequestCommand>();
            requestBuilderMock.Setup(m => m.CanHandle(parameters.Object)).Returns(false);

            // Act
            PrecedingRequestCommandFactory target = new PrecedingRequestCommandFactory(requestBuilderMock.Object);
            var actual = target.Execute(parameters.Object);

            // Assert
            actual.Should().Be.True();
            requestBuilderMock.Verify(m => m.Process(parameters.Object), Times.Never);
            requestBuilderMock.Verify(m => m.CanHandle(parameters.Object), Times.Once);
        }

        [TestMethod]
        public void Execute_WhenCalledWithRequestCommandThatReturnsTrue_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestCommand> requestBuilderMock = new Mock<IRequestCommand>();
            requestBuilderMock.Setup(m => m.Process(parameters.Object)).Returns(true);
            requestBuilderMock.Setup(m => m.CanHandle(parameters.Object)).Returns(true);

            // Act
            PrecedingRequestCommandFactory target = new PrecedingRequestCommandFactory(requestBuilderMock.Object);
            var actual = target.Execute(parameters.Object);

            // Assert
            actual.Should().Be.True();
            requestBuilderMock.Verify(m => m.Process(parameters.Object), Times.Once);
            requestBuilderMock.Verify(m => m.CanHandle(parameters.Object), Times.Once);
        }

        [TestMethod]
        public void Execute_WhenCalledWithRequestCommandThatReturnsFalse_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestCommand> requestBuilderMock = new Mock<IRequestCommand>();
            requestBuilderMock.Setup(m => m.Process(parameters.Object)).Returns(false);
            requestBuilderMock.Setup(m => m.CanHandle(parameters.Object)).Returns(true);

            // Act
            PrecedingRequestCommandFactory target = new PrecedingRequestCommandFactory(requestBuilderMock.Object);
            var actual = target.Execute(parameters.Object);

            // Assert
            actual.Should().Be.False();
            requestBuilderMock.Verify(m => m.Process(parameters.Object), Times.Once);
            requestBuilderMock.Verify(m => m.CanHandle(parameters.Object), Times.Once);
        }
    }
}
