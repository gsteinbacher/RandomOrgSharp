using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.UnitTest.Tools;
using Obacher.UnitTest.Tools.Mocks;
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
            const int id = 999;

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
    }
}
