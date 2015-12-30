using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Method;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.BasicMethod
{
    [TestClass]
    public class StringBasicMethodTest
    {
        [TestMethod]
        public void WhenGenerateStringsCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            const string charactersAllowed = "abc";
            const bool allowDuplicates = false;

            var expected = new DataResponse<string>(null, Enumerable.Empty<string>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IDataMethodManager<string>> basicMethodMock = new Mock<IDataMethodManager<string>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IParameters>())).Returns(expected);

            var target = new StringMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = target.GenerateStrings(numberOfItems, length, charactersAllowed, allowDuplicates);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod]
        public async Task WhenGenerateStringsAsyncCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            const string charactersAllowed = "abc";
            const bool allowDuplicates = false;

            var expected = new DataResponse<string>(null, Enumerable.Empty<string>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IDataMethodManager<string>> basicMethodMock = new Mock<IDataMethodManager<string>>();
            basicMethodMock.Setup(m => m.GenerateAsync(It.IsAny<IParameters>())).ReturnsAsync(expected);

            var target = new StringMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = await target.GenerateStringsAsync(numberOfItems, length, charactersAllowed, allowDuplicates);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod]
        public void WhenGenerateStringsCalledWithCharactersAllowedEnum_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            const CharactersAllowed charactersAllowed = CharactersAllowed.Alpha;
            const bool allowDuplicates = false;

            var expected = new DataResponse<string>(null, Enumerable.Empty<string>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IDataMethodManager<string>> basicMethodMock = new Mock<IDataMethodManager<string>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IParameters>())).Returns(expected);

            var target = new StringMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = target.GenerateStrings(numberOfItems, length, charactersAllowed, allowDuplicates);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod]
        public async Task WhenGenerateStringsAsyncCalledWithCharactersAllowedEnum_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            const CharactersAllowed charactersAllowed = CharactersAllowed.Alpha; const bool allowDuplicates = false;

            var expected = new DataResponse<string>(null, Enumerable.Empty<string>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IDataMethodManager<string>> basicMethodMock = new Mock<IDataMethodManager<string>>();
            basicMethodMock.Setup(m => m.GenerateAsync(It.IsAny<IParameters>())).ReturnsAsync(expected);

            var target = new StringMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = await target.GenerateStringsAsync(numberOfItems, length, charactersAllowed, allowDuplicates);

                // Assert
                actual.Should().Equal(expected);
            }
        }
    }
}
