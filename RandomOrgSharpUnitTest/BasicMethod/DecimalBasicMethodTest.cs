﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Method;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.BasicMethod
{
    [TestClass]
    public class DecimalBasicMethodTest
    {
        [TestMethod]
        public void WhenGenerateDecimalFractionsCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = 10;

            var expected = new DataResponse<decimal>(null, Enumerable.Empty<decimal>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IDataMethodManager<decimal>> basicMethodMock = new Mock<IDataMethodManager<decimal>>();
            basicMethodMock.Setup(m => m.Generate(It.IsAny<IParameters>())).Returns(expected);

            var target = new DecimalMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = target.GenerateDecimalFractions(numberOfItems, numberOfdecimalPlaces);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod]
        public async Task WhenGenerateDecimalFractionsAsyncCalled_ExpectNoException()
        {
            // Arrange
            const int numberOfItems = 1;
            const int numberOfdecimalPlaces = 10;

            var expected = new DataResponse<decimal>(null, Enumerable.Empty<decimal>(), DateTime.Now, 0, 0, 0, 0, 0);

            Mock<IDataMethodManager<decimal>> basicMethodMock = new Mock<IDataMethodManager<decimal>>();
            basicMethodMock.Setup(m => m.GenerateAsync(It.IsAny<IParameters>())).ReturnsAsync(expected);

            var target = new DecimalMethod(basicMethodMock.Object);
            using (new MockCommonParameters())
            {
                // Act
                var actual = await target.GenerateDecimalFractionsAsync(numberOfItems, numberOfdecimalPlaces);

                // Assert
                actual.Should().Equal(expected);
            }
        }
    }
}