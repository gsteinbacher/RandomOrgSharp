using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.UnitTest.Tools;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Request
{
    [TestClass]
    public class JsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof (ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            Mock<IJsonRequestBuilder> requestBuilderMock = new Mock<IJsonRequestBuilder>();

            // Arrange
            var target = new JsonRequestBuilder(requestBuilderMock.Object);
            target.Build(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof (ArgumentException), "CommonParameters")]
        public void Create_WhenParametersNotTypeOfBlobParameter_ExpectException()
        {
            // Arrange
            Mock<IParameters> parameters = new Mock<IParameters>();
            Mock<IJsonRequestBuilder> requestBuilderMock = new Mock<IJsonRequestBuilder>();

            var target = new JsonRequestBuilder(requestBuilderMock.Object);
            target.Build(parameters.Object);

            // Assert
        }

        [TestMethod]
        public void Create_WhenBuildReturnsNull_ExpectEmptyParametersNode()
        {
            // Act
            const int id = 999;
            const string apiKey = "Test_APIKEY";

            var settingsManagerMock = MockHelper.MockSettingsManager(apiKey);
            RandomNumberGenerator.Instance = MockHelper.SetupIdMock(id).Object;

            var expected = new JObject(
                new JProperty(JsonRpcConstants.RPC_PARAMETER_NAME, JsonRpcConstants.RPC_VALUE),
                new JProperty(JsonRpcConstants.METHOD_PARAMETER_NAME, "generateDecimalFractions"),
                new JProperty(JsonRpcConstants.PARAMETERS_PARAMETER_NAME,
                    new JObject(
                        new JProperty(RandomOrgConstants.APIKEY_KEY, apiKey))
                    ),
                new JProperty(JsonRpcConstants.ID_PARAMETER_NAME, id)
                );

            // Act
            CommonParameters parameters = new CommonParameters(MethodType.Decimal);
            Mock<IJsonRequestBuilder> mockBuilder = new Mock<IJsonRequestBuilder>();
            mockBuilder.Setup(m => m.Build(parameters)).Returns((JObject) null);

            var target = new JsonRequestBuilder(mockBuilder.Object, settingsManagerMock.Object);
            var actual = target.Build(parameters);

            // Assert
            actual.Should().Equal(expected.ToString());

        }
    }
}