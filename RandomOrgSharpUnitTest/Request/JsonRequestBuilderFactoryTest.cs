using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Request
{
    [TestClass]
    public class JsonRequestBuilderFactoryTest
    {
        [TestMethod]
        public void GetBuilder_WhenBuilderFound_ExpectNonDefaultBuilderReturned()
        {
            // Arrange
            Mock<IParameters> mockParameters = new Mock<IParameters>();
            Mock<IJsonRequestBuilder> mockDefaultBuilder = new Mock<IJsonRequestBuilder>();
            Mock<IJsonRequestBuilder> mockBuilder = new Mock<IJsonRequestBuilder>();
            mockBuilder.Setup(m => m.CanHandle(mockParameters.Object)).Returns(true);

            // Act
            JsonRequestBuilderFactory target = new JsonRequestBuilderFactory(mockDefaultBuilder.Object, mockBuilder.Object);
            var actual = target.GetBuilder(mockParameters.Object);

            // Assert
            actual.Should().Equal(mockBuilder.Object);
        }

        [TestMethod]
        public void GetBuilder_WhenBuilderNotFound_ExpectDefaultBuilderReturned()
        {
            // Arrange
            Mock<IJsonRequestBuilder> mockDefaultBuilder = new Mock<IJsonRequestBuilder>();
            Mock<IJsonRequestBuilder> mockBuilder = new Mock<IJsonRequestBuilder>();

            Mock<IParameters> mockParameters = new Mock<IParameters>();

            // Act
            JsonRequestBuilderFactory target = new JsonRequestBuilderFactory(mockDefaultBuilder.Object, mockBuilder.Object);
            var actual = target.GetBuilder(mockParameters.Object);

            // Assert
            actual.Should().Equal(mockDefaultBuilder.Object);
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void GetBuilder_WhenNullPassed_ExpectException()
        {
            // Arrange
            Mock<IJsonRequestBuilder> mockDefaultBuilder = new Mock<IJsonRequestBuilder>();
            Mock<IJsonRequestBuilder> mockBuilder = new Mock<IJsonRequestBuilder>();

            // Act
            JsonRequestBuilderFactory target = new JsonRequestBuilderFactory(mockDefaultBuilder.Object, mockBuilder.Object);
            target.GetBuilder(null);

            // Assert
        }


        [TestMethod]
        public void GetDefaultBuilders_WhenCalled_ExpectNoException()
        {
            var builders = JsonRequestBuilderFactory.GetDefaultBuilders();

            builders.Should().Be.AssignableFrom(typeof(IJsonRequestBuilderFactory));
        }
    }
}
