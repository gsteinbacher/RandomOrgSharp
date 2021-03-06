﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
{
    [TestClass]
    public class UuidParametersTest
    {
        [TestMethod, ExpectedException(typeof(RandomOrgRuntimeException))]
        public void WhenNumberOfItemsToReturnLessThanMinimumAllowed_ExpectException()
        {
            // Arrange
            const int numberOfItems = -1;

            // Act
            UuidParameters.Create(numberOfItems);
        }

        [TestMethod, ExpectedException(typeof(RandomOrgRuntimeException))]
        public void WhenNumberOfItemsToReturnGreaterThenMaximumAllowed_ExpectException()
        {
            const int numberOfItems = 1001;

            // Act
            UuidParameters.Create(numberOfItems);
        }

        [TestMethod]
        public void WhenAllValuesValid_ExpectValuesSet()
        {
            // Arrange
            const int numberOfItems = 1;

            UuidParameters result;

            // Act
            result = UuidParameters.Create(numberOfItems);

            // Assert
            result.NumberOfItemsToReturn.Should().Equal(numberOfItems);
        }
    }
}
