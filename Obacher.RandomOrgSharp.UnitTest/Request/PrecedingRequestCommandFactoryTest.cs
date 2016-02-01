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
            BeforeRequestCommandFactory target = new BeforeRequestCommandFactory();
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
            requestBuilderMock.Setup(m => m.CanProcess(parameters.Object)).Returns(false);

            // Act
            BeforeRequestCommandFactory target = new BeforeRequestCommandFactory(requestBuilderMock.Object);
            var actual = target.Execute(parameters.Object);

            // Assert
            actual.Should().Be.True();
            requestBuilderMock.Verify(m => m.Process(parameters.Object), Times.Never);
            requestBuilderMock.Verify(m => m.CanProcess(parameters.Object), Times.Once);
        }

        [TestMethod]
        public void Execute_WhenCalledWithRequestCommandThatReturnsTrue_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestCommand> requestBuilderMock = new Mock<IRequestCommand>();
            requestBuilderMock.Setup(m => m.Process(parameters.Object)).Returns(true);
            requestBuilderMock.Setup(m => m.CanProcess(parameters.Object)).Returns(true);

            // Act
            BeforeRequestCommandFactory target = new BeforeRequestCommandFactory(requestBuilderMock.Object);
            var actual = target.Execute(parameters.Object);

            // Assert
            actual.Should().Be.True();
            requestBuilderMock.Verify(m => m.Process(parameters.Object), Times.Once);
            requestBuilderMock.Verify(m => m.CanProcess(parameters.Object), Times.Once);
        }

        [TestMethod]
        public void Execute_WhenCalledWithRequestCommandThatReturnsFalse_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IRequestCommand> requestBuilderMock = new Mock<IRequestCommand>();
            requestBuilderMock.Setup(m => m.Process(parameters.Object)).Returns(false);
            requestBuilderMock.Setup(m => m.CanProcess(parameters.Object)).Returns(true);

            // Act
            BeforeRequestCommandFactory target = new BeforeRequestCommandFactory(requestBuilderMock.Object);
            var actual = target.Execute(parameters.Object);

            // Assert
            actual.Should().Be.False();
            requestBuilderMock.Verify(m => m.Process(parameters.Object), Times.Once);
            requestBuilderMock.Verify(m => m.CanProcess(parameters.Object), Times.Once);
        }
    }
}
