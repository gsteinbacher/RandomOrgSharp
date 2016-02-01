using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest
{
    [TestClass]
    public class RandomOrgExceptionTest
    {
        [TestMethod]
        public void WhenEmptyConstructorCalled_ExpectPropertiesSetCorrectly()
        {
            // Arrange
            const int expectedCode = 32000;

            // Act
            RandomOrgException target = new RandomOrgException();

            // Assert
            target.Code.Should().Equal(expectedCode);
        }

        [TestMethod]
        public void WhenConstructorCalled_ExpectPropertiesSetCorrectly()
        {
            // Arrange
            const int expectedCode = 400;
            const string expectedMessage = "Test Message";

            // Act
            RandomOrgException target = new RandomOrgException(expectedCode, expectedMessage);

            // Assert
            target.Code.Should().Equal(expectedCode);
            target.Message.Should().Equal(expectedMessage);
        }

        [TestMethod]
        public void WhenConstructorCalledWithInnerException_ExpectPropertiesSetCorrectly()
        {
            // Arrange
            const int expectedCode = 400;
            const string expectedMessage = "Test Message";
            var innerException = new ArgumentNullException();

            // Act
            RandomOrgException target = new RandomOrgException(expectedCode, expectedMessage, innerException);

            // Assert
            target.Code.Should().Equal(expectedCode);
            target.Message.Should().Equal(expectedMessage);
            target.InnerException.Should().Be.OfType(typeof(ArgumentNullException));
        }

        [TestMethod]
        public void WhenConstructorCalledWithSerialization_ExpectPropertiesSetCorrectly()
        {
            // Arrange
            const int expectedCode = 400;
            const string expectedMessage = "Test Message";

            // Act
            RandomOrgException target = new RandomOrgException(expectedCode, expectedMessage);

            IFormatter formatter = new SoapFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, target);
            stream.Position = 0;

            using (var sr = new StreamReader(stream))
            {
                var actualMessage = sr.ReadToEnd();

                // Assert
                actualMessage.Should().Contain(expectedMessage);

                stream.Position = 0;
                RandomOrgException ex = formatter.Deserialize(stream) as RandomOrgException;
                ex.Code.Should().Equal(expectedCode);
                ex.Message.Should().Equal(expectedMessage);
            }

        }
    }
}