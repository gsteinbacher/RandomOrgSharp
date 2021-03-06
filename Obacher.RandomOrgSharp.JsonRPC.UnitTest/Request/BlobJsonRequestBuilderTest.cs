﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Request
{
    [TestClass]
    public class BlobJsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new BlobJsonRequestBuilder();
            target.Build(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "BlobParameters")]
        public void Create_WhenParametersNotTypeOfBlobParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            var target = new BlobJsonRequestBuilder();
            target.Build(parameters.Object);

            // Assert
        }

        [TestMethod]
        public void Create_WhenParametersCorrect_ExpectJsonReturned()
        {
            // Arrange
            const int numberOfItems = 1;
            const int size = 8;
            const BlobFormat format = BlobFormat.Hex;

            JObject expected =
                new JObject(
                    new JProperty(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(JsonRpcConstants.SIZE_PARAMETER_NAME, size),
                    new JProperty(JsonRpcConstants.FORMAT_PARAMETER_NAME, format.ToString().ToLowerInvariant())
                    );

            var parameters = BlobParameters.Create(numberOfItems, size, format);
            var target = new BlobJsonRequestBuilder();
            var actual = target.Build(parameters);

            // Assert
            actual.Should().Equal(expected);
        }


        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void CanHandle_WhenParametersNull_ExpectException()
        {
            // Arrange

            // Act
            var target = new BlobJsonRequestBuilder();
            target.CanHandle(null);

            // Assert
        }

        [TestMethod]
        public void CanHandle_WhenParametersMethodTypeIsGuassian_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(m => m.MethodType).Returns(MethodType.Blob);

            // Act
            var target = new BlobJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenParametersMethodTypeIsNotGuassian_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(m => m.MethodType).Returns(MethodType.Decimal);

            // Act
            var target = new BlobJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Be.False();
        }
    }
}
