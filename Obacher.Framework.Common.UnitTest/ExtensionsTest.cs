using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should.Fluent;

namespace Obacher.Framework.Common.UnitTest
{
    [TestClass()]
    public class ExtensionsTest
    {
        [TestMethod()]
        public void Between_WhenValueIsBetween_ExpectTrue()
        {
            // Arrange
            const int testValue = 15;
            const bool expected = true;
            const int startRange = 10;
            const int endRange = 20;

            // Act
            bool actual = testValue.Between(startRange, endRange);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod()]
        public void Between_WhenValueIsLessThanRange_ExpectFalse()
        {
            // Arrange
            const int testValue = 5;
            const bool expected = false;
            const int startRange = 10;
            const int endRange = 20;

            // Act
            bool actual = testValue.Between(startRange, endRange);

            // Assert
            actual.Should().Equal(expected);
        }


        [TestMethod()]
        public void Between_WhenValueIsGreaterThanRange_ExpectFalse()
        {
            // Arrange
            const int testValue = 25;
            const bool expected = false;
            const int startRange = 10;
            const int endRange = 20;

            // Act
            bool actual = testValue.Between(startRange, endRange);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}