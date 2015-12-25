using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.RequestParameters
{
    [TestClass]
    public class StringRequestParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            const int length = 10;
            const string charactersAllowed = "abc";
            using (new MockCommonParameters())

                // Act
                StringParameters.Set(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 10001;
            const int length = 10;
            const string charactersAllowed = "abc";
            using (new MockCommonParameters())

                // Act
                StringParameters.Set(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenLengthLessThenMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 0;
            const string charactersAllowed = "abc";
            using (new MockCommonParameters())

                // Act
                StringParameters.Set(numberOfItems, length, charactersAllowed);
        }


        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenLengthGreaterThenMaximumllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 21;
            const string charactersAllowed = "abc";
            using (new MockCommonParameters())

                // Act
                StringParameters.Set(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenCharactersAllowedIsEmpty_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            string charactersAllowed = string.Empty;
            using (new MockCommonParameters())

                // Act
                StringParameters.Set(numberOfItems, length, charactersAllowed);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenCharactersAllowedLengthGreaterThenMaximumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            string charactersAllowed = new string('a', 81);
            using (new MockCommonParameters())

                // Act
                StringParameters.Set(numberOfItems, length, charactersAllowed);
        }

        [TestMethod]
        public void WhenAllValuesValid_ExpectValuesSet()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            string charactersAllowed = "ABCDEFGHIJKLabdefgstuvw012389";
            const bool allowDuplicates = false;


            StringParameters result;
            using (new MockCommonParameters())

                // Act
                result = StringParameters.Set(numberOfItems, length, charactersAllowed, allowDuplicates);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
            result.Length.Should().Equal(length);
            result.Allowed.Should().Equal(charactersAllowed);
            result.AllowDuplicates.Should().Equal(allowDuplicates);
        }
    }
}
