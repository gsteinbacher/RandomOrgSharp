using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Core;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest
{
    [TestClass]
    public class RandomOrgRuntimeExceptionTest
    {
        [TestMethod]
        public void WhenConstructorCalled_ExpectPropertiesSetCorrectly()
        {
            // Arrange
            const string expectedMessage = "Test Message";

            // Act
            RandomOrgRuntimeException target = new RandomOrgRuntimeException(expectedMessage);

            // Assert
            target.Message.Should().Equal(expectedMessage);
        }

        [TestMethod]
        public void WhenConstructorCalledWithInnerException_ExpectPropertiesSetCorrectly()
        {
            // Arrange
            const string expectedMessage = "Test Message";
            var innerException = new ArgumentNullException();

            // Act
            RandomOrgRuntimeException target = new RandomOrgRuntimeException(expectedMessage, innerException);

            // Assert
            target.Message.Should().Equal(expectedMessage);
            target.InnerException.Should().Be.OfType(typeof(ArgumentNullException));
        }

        [TestMethod]
        public void WhenConstructorCalledWithSerialization_ExpectPropertiesSetCorrectly()
        {
            // Act
            RandomOrgRuntimeException target = new RandomOrgRuntimeException();

            IFormatter formatter = new SoapFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, target);
                stream.Position = 0;

                using (var sr = new StreamReader(stream))
                {
                    var actualMessage = sr.ReadToEnd();

                    // Assert
                    actualMessage.Should().Contain("RandomOrgRuntimeException");

                    stream.Position = 0;
                    RandomOrgRuntimeException ex = formatter.Deserialize(stream) as RandomOrgRuntimeException;
                    ex.Should().Not.Be.Null();
                    ex?.Message.Should().Contain("RandomOrgRuntimeException");
                }
            }
        }
    }
}