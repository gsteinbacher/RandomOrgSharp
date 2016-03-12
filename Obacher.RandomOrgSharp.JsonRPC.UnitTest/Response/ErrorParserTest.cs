using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Response;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Response
{
    [TestClass]
    public class ErrorParserTest
    {
        [TestMethod]
        public void Parse_WhenNullInput_ShouldReturnEmptyErrorResponseInfo()
        {
            // Arrange
            string input = null;
            var expected = ErrorResponseInfo.Empty();

            // Act
            ErrorParser target = new ErrorParser();
            var actual = target.Parse(input);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void Parse_WhenEmptyInput_ShouldReturnEmptyErrorResponseInfo()
        {
            // Arrange
            string input = null;
            var expected = ErrorResponseInfo.Empty();

            // Act
            ErrorParser target = new ErrorParser();
            var actual = target.Parse(input);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void Parse_WhenResponseDoesNotHaveErrorNode_ShouldReturnEmptyErrorResponseInfo()
        {
            // Arrange
            string expectedVersion = "2.0";
            var expected = ErrorResponseInfo.Empty(expectedVersion);

            var input = new JObject(
                new JProperty("jsonrpc", expectedVersion),
                new JProperty("noterrorNode")
                );

            // Act
            ErrorParser target = new ErrorParser();
            var actual = target.Parse(input.ToString());

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void Parse_WhenCalledWithErrorJson_ShouldReturnPopulatedErrorResponseInfo()
        {
            // Arrange
            const string expectedVersion = "2.0";
            const int expectedCode = 1111111;
            const string expectedMessage = "Test Message";
            const string expectedData = null;
            int expectedId = RandomGenerator.GetInteger(1);

            var input = new JObject(
                new JProperty("jsonrpc", expectedVersion),
                new JProperty("error",
                    new JObject(
                        new JProperty("code", expectedCode.ToString().ToLower()),
                        new JProperty("message", expectedMessage),
                        new JProperty("data", expectedData)
                    )),
                new JProperty("id", expectedId)
                );

            // Act
            ErrorParser target = new ErrorParser();
            var actual = target.Parse(input.ToString()) as ErrorResponseInfo;

            // Arrange
            actual.Should().Not.Be.Null();
            actual.Code.Should().Equal(expectedCode);
            actual.Message.Should().Equal(expectedMessage);
            actual.Id.Should().Equal(expectedId);
            actual.AdvisoryDelay.Should().Equal(0);
        }

        [TestMethod]
        public void Parse_WhenErrorCodeNotFoundInResourceFile_ShouldReturnMessageFromJson()
        {
            // Arrange
            const string version = "2.0";
            const int code = 1111111;
            const string message = "Test Message";
            const string data = null;
            int id = RandomGenerator.GetInteger(1);

            const string expected = message;

            var input = new JObject(
                new JProperty("jsonrpc", version),
                new JProperty("error",
                    new JObject(
                        new JProperty("code", code.ToString().ToLower()),
                        new JProperty("message", message),
                        new JProperty("data", data)
                    )),
                new JProperty("id", id)
                );

            // Act
            ErrorParser target = new ErrorParser();
            var actual = target.Parse(input.ToString()) as ErrorResponseInfo;

            // Arrange
            actual.Should().Not.Be.Null();
            actual.Message.Should().Equal(expected);
        }

        [TestMethod]
        public void Parse_WhenDataNotNull_ShouldReturnMessageFormatted()
        {
            // Arrange
            const string version = "2.0";
            const int code = 11111;
            string message = "Test Message with {0}";
            const string data = "Test Data";
            int expectedId = RandomGenerator.GetInteger(1);
            string expected = string.Format(message, data);

            var input = new JObject(
                new JProperty("jsonrpc", version),
                new JProperty("error",
                    new JObject(
                        new JProperty("code", code.ToString().ToLower()),
                        new JProperty("message", message),
                        new JProperty("data", new JArray(data))
                    )),
                new JProperty("id", expectedId)
                );

            // Act
            ErrorParser target = new ErrorParser();
            var actual = target.Parse(input.ToString()) as ErrorResponseInfo;

            // Arrange
            actual.Should().Not.Be.Null();
            actual.Message.Should().Equal(expected);
        }

        [TestMethod]
        public void Parse_WhenErrorCodeFoundInResourceFile_ShouldReturnMessageFromResourceFile()
        {
            // Arrange
            const string version = "2.0";
            const int code = 400;
            const string message = "Test Message";
            const string data = null;
            int id = RandomGenerator.GetInteger(1);

            var input = new JObject(
                new JProperty("jsonrpc", version),
                new JProperty("error",
                    new JObject(
                        new JProperty("code", code.ToString().ToLower()),
                        new JProperty("message", message),
                        new JProperty("data", data)
                    )),
                new JProperty("id", id)
                );

            // Act
            ErrorParser target = new ErrorParser();
            var actual = target.Parse(input.ToString()) as ErrorResponseInfo;

            // Arrange
            actual.Should().Not.Be.Null();
            actual.Message.Should().Not.Equal(message);         // Should be message from resource file, not from JSON
        }


        [TestMethod]
        public void Parse_WhenErrorCodeFoundInResourceFileAndDataExists_ShouldReturnFormattedMessageFromResourceFile()
        {
            // Arrange
            const string version = "2.0";
            const int code = 200;
            const string message = "Test Message";
            IList<string> data = new List<string>() {"n"};
            int id = RandomGenerator.GetInteger(1);

            var input = new JObject(
                new JProperty("jsonrpc", version),
                new JProperty("error",
                    new JObject(
                        new JProperty("code", code.ToString().ToLower()),
                        new JProperty("message", message),
                        new JProperty("data", data)
                    )),
                new JProperty("id", id)
                );

            // Act
            ErrorParser target = new ErrorParser();
            var actual = target.Parse(input.ToString()) as ErrorResponseInfo;

            // Arrange
            actual.Should().Not.Be.Null();
            actual.Message.Should().Not.Equal(message);         // Should be message from resource file, not from JSON
        }

        [TestMethod]
        public void CanHandle_WheCalled_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Usage);

            // Act
            ErrorParser target = new ErrorParser();
            var actual = target.CanParse(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }
    }
}
