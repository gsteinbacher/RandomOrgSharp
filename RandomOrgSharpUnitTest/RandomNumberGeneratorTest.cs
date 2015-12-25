using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest
{
    [TestClass]
    public class RandomNumberGeneratorTest
    {
        [TestMethod]
        public void Next_WhenCalled_ExpectNoException()
        {
            RandomNumberGenerator.Instance.Next();
        }

        [TestMethod]
        public void Next_WhenMocked_ExpectMockObjectUsed()
        {
            // Arrange
            const int expected = 10000001;
            Mock<IRandom> randomMock = new Mock<IRandom>();
            randomMock.Setup(m => m.Next()).Returns(expected);
            RandomNumberGenerator.Instance = randomMock.Object;

            // Act
            var actual = RandomNumberGenerator.Instance.Next();

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
