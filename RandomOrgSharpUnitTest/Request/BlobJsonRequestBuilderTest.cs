﻿using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Obacher.UnitTest.Tools;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Request
{
    [TestClass]
    public class BlobJsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new BlobJsonRequestBuilder();
            target.Create(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "BlobParameters")]
        public void Create_WhenParametersNotTypeOfBlobParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            var target = new BlobJsonRequestBuilder();
            target.Create(parameters.Object);

            // Assert
        }

        [TestMethod]
        public void Create_WhenParametersCorrect_ExpectJsonReturned()
        {
            // Arrange
            const int numberOfItems = 1;
            const int size = 8;
            const BlobFormat format = BlobFormat.Hex;
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(RandomOrgConstants.JSON_SIZE_PARAMETER_NAME, size),
                    new JProperty(RandomOrgConstants.JSON_FORMAT_PARAMETER_NAME, format.ToString().ToLowerInvariant())
                    );

            using (new MockCommonParameters(id))
            {
                var parameters = BlobParameters.Create(numberOfItems, size, format);
                var target = new BlobJsonRequestBuilder();
                var actual = target.Create(parameters);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void CanHandle_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new BlobJsonRequestBuilder();
            target.CanHandle(null);

            // Assert
        }

        [TestMethod]
        public void CanHandle_WhenMethodTypeIsBlob_ExpectTrue()
        {
            // Arrange
            const bool expected = true;
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(p => p.MethodType).Returns(MethodType.Blob);

            // Act
            var target = new BlobJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void CanHandle_WhenMethodTypeIsNotBlob_ExpectFalse()
        {
            // Arrange
            const bool expected = false;
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(p => p.MethodType).Returns(MethodType.Decimal);

            // Act
            var target = new BlobJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
