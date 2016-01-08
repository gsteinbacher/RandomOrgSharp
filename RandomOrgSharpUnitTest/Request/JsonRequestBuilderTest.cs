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
    public class JsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            // Arrange
            var target = new JsonRequestBuilder();
            target.Create(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "CommonParameters")]
        public void Create_WhenParametersNotTypeOfBlobParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();

            var target = new JsonRequestBuilder();
            target.Create(parameters.Object);

            // Assert
        }

        [TestMethod]
        public void Create_WhenParametersCorrect_ExpectJsonReturned()
        {
            // Act
            const int id = 999;

            var expected = new JObject(
               new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
               new JProperty(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME, "generateDecimalFractions"),
               new JProperty(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME, new JObject(
                    new JProperty(RandomOrgConstants.APIKEY_KEY, ConfigMocks.MOCK_API_KEY))),
               new JProperty(RandomOrgConstants.JSON_ID_PARAMETER_NAME, id)
               );

            // Act
            using (new MockCommonParameters(id))
            {
                CommonParameters parameters = new CommonParameters(MethodType.Decimal);
                Mock<IJsonRequestBuilder> mockBuilder = new Mock<IJsonRequestBuilder>();
                Mock<IJsonRequestBuilderFactory> mockFactory = new Mock<IJsonRequestBuilderFactory>();
                mockFactory.Setup(m => m.GetBuilder(parameters)).Returns(mockBuilder.Object);

                var target = new JsonRequestBuilder(mockFactory.Object);
                var actual = target.Create(parameters);

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [TestMethod]
        public void CanHandle_WhenCalled_ExpectFalse()
        {
            // Arrange
            const bool expected = false;

            // Act
            var target = new JsonRequestBuilder();
            var actual = target.CanHandle(null);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}

