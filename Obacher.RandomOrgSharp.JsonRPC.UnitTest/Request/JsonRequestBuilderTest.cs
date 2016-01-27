using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.UnitTest.Tools;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Request
{
    [TestClass]
    public class JsonRequestBuilderTest
    {
        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void Create_WhenParametersNull_ExpectException()
        {
            Mock<IJsonRequestBuilder> requestBuilderMock = new Mock<IJsonRequestBuilder>();

            // Arrange
            var target = new JsonRequestBuilder(requestBuilderMock.Object);
            target.Build(null);

            // Assert
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentException), "CommonParameters")]
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
        public void Create_WhenParametersCorrect_ExpectJsonReturned()
        {
            //// Act
            //const int id = 999;

            //var expected = new JObject(
            //   new JProperty(JsonRpcConstants.RPC_PARAMETER_NAME, JsonRpcConstants.RPC_VALUE),
            //   new JProperty(JsonRpcConstants.METHOD_PARAMETER_NAME, "generateDecimalFractions"),
            //   new JProperty(JsonRpcConstants.PARAMETERS_PARAMETER_NAME, new JObject(
            //        new JProperty(RandomOrgConstants.APIKEY_KEY, ConfigMocks.MOCK_API_KEY))),
            //   new JProperty(JsonRpcConstants.ID_PARAMETER_NAME, id)
            //   );

            //// Act
            //using (new MockCommonParameters(id))
            //{
            //    CommonParameters parameters = new CommonParameters(MethodType.Decimal);
            //    Mock<IJsonRequestBuilder> mockBuilder = new Mock<IJsonRequestBuilder>();
            //    Mock<IResponseHandler> mockFactory = new Mock<IResponseHandler>();
            //    mockFactory.Setup(m => m.Handle(parameters)).Returns(mockBuilder.Object);

            //    var target = new JsonRequestBuilder(mockFactory.Object);
            //    var actual = target.Build(parameters);

            //    // Assert
            //    actual.Should().Equal(expected);
        }
    }
}


