﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
{
    [TestClass]
    public class UuidParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;
            using (new MockCommonParameters())

                // Act
                UuidParameters.Create(numberOfItems);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRunTimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 1001;
            using (new MockCommonParameters())

                // Act
                UuidParameters.Create(numberOfItems);
        }

        [TestMethod]
        public void WhenAllValuesValid_ExpectValuesSet()
        {
            // Arrange
            const int numberOfItems = 1;

            UuidParameters result;
            using (new MockCommonParameters())

                // Act
                result = UuidParameters.Create(numberOfItems);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
        }
    }
}