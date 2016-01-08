using System;
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
    public class StringJsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new StringJsonRequestBuilder();
            target.Create(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "StringParameters")]
        public void Create_WhenParametersNotTypeOfStringParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            var target = new StringJsonRequestBuilder();
            target.Create(parameters.Object);

            // Assert
        }

        [TestMethod]
        public void WhenParametersCorrect_ExpectJsonReturned()
        {
            // Arrange
            const int numberOfItems = 1;
            const int length = 10;
            const string charactersAllowed = "abc";
            const bool allowDuplicates = false;
            const int id = 999;

            JObject expected =
                new JObject(
                    new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, numberOfItems),
                    new JProperty(RandomOrgConstants.JSON_LENGTH_PARAMETER_NAME, length),
                    new JProperty(RandomOrgConstants.JSON_CHARACTERS_ALLOWED_PARAMETER_NAME, charactersAllowed),
                    new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, allowDuplicates)
                );

            // Act
            using (new MockCommonParameters(id))
            {
                var parameters = StringParameters.Create(numberOfItems, length, charactersAllowed, allowDuplicates);
                var target = new StringJsonRequestBuilder();
                var actual = target.Create(parameters);

                // Assert
                actual.Should().Equal(expected);
            }
        }
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void CanHandle_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new StringJsonRequestBuilder();
            target.CanHandle(null);

            // Assert
        }

        [TestMethod]
        public void CanHandle_WhenMethodTypeIsString_ExpectTrue()
        {
            // Arrange
            const bool expected = true;
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(p => p.MethodType).Returns(MethodType.String);

            // Act
            var target = new StringJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void CanHandle_WhenMethodTypeIsNotString_ExpectFalse()
        {
            // Arrange
            const bool expected = false;
            Mock<IParameters> parameters = new Mock<IParameters>();
            parameters.Setup(p => p.MethodType).Returns(MethodType.Blob);

            // Act
            var target = new StringJsonRequestBuilder();
            var actual = target.CanHandle(parameters.Object);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
