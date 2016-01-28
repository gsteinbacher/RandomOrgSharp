using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
{
    [TestClass]
    public class StringParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int length = 10;
            const string charactersAllowed = "abc";

            // Act
            StringParameters.Create(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 10001;
            const int length = 10;
            const string charactersAllowed = "abc";

            // Act
            StringParameters.Create(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenLengthLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 0;
            const string charactersAllowed = "abc";

            // Act
            StringParameters.Create(numberOfItems, length, charactersAllowed);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenLengthGreaterThenMaximumllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 21;
            const string charactersAllowed = "abc";

            // Act
            StringParameters.Create(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenCharactersAllowedIsEmpty_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            string charactersAllowed = string.Empty;

            // Act
            StringParameters.Create(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenCharactersAllowedLengthGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            string charactersAllowed = new string('a', 81);

            // Act
            StringParameters.Create(numberOfItems, length, charactersAllowed);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenCharactersAllowedNull_ExpectCharactersAllowedToReturnEmptyString()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            const string charactersAllowed = null;
            string expectedCharactersAllowed = string.Empty;

            // Act
            var paramaters = StringParameters.Create(numberOfItems, length, charactersAllowed);
            paramaters.CharactersAllowed.Should().Equal(expectedCharactersAllowed);
        }


        [TestMethod]
        public void WhenAllParametersAllowed_ExpectNOException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            const string charactersAllowed = "ABCDEFGabcdefg";

            // Act
            var actual = StringParameters.Create(numberOfItems, length, charactersAllowed);

            // Assert
            actual.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            actual.Length.Should().Equal(length);
            actual.CharactersAllowed.Should().Equal(charactersAllowed);
            actual.AllowDuplicates.Should().Be.True();
        }

        [TestMethod]
        public void WhenCharactersAllowedSet_ExpectValidCharactersAllowedReturned()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;

            // Act
            var paramaters = StringParameters.Create(numberOfItems, length, CharactersAllowed.Alpha);
            paramaters.CharactersAllowed.Should().Equal("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");

            paramaters = StringParameters.Create(numberOfItems, length, CharactersAllowed.UpperOnly);
            paramaters.CharactersAllowed.Should().Equal("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

            paramaters = StringParameters.Create(numberOfItems, length, CharactersAllowed.LowerOnly);
            paramaters.CharactersAllowed.Should().Equal("abcdefghijklmnopqrstuvwxyz");

            paramaters = StringParameters.Create(numberOfItems, length, CharactersAllowed.UpperNumeric);
            paramaters.CharactersAllowed.Should().Equal("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

            paramaters = StringParameters.Create(numberOfItems, length, CharactersAllowed.LowerNumeric);
            paramaters.CharactersAllowed.Should().Equal("abcdefghijklmnopqrstuvwxyz0123456789");

            paramaters = StringParameters.Create(numberOfItems, length, CharactersAllowed.AlphaNumeric);
            paramaters.CharactersAllowed.Should().Equal("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
        }

        [TestMethod]
        public void WhenAllValuesValid_ExpectValuesSet()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            const CharactersAllowed charactersAllowed = CharactersAllowed.UpperOnly;
            const bool allowDuplicates = false;

            const string expectedAllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // Act
            var result = StringParameters.Create(numberOfItems, length, charactersAllowed, allowDuplicates);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            result.Length.Should().Equal(length);
            result.CharactersAllowed.Should().Equal(expectedAllowedCharacters);
            result.AllowDuplicates.Should().Equal(allowDuplicates);
        }
    }
}
