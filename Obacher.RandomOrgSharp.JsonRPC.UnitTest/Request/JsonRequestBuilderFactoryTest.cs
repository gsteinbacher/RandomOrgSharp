using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Request
{
    [TestClass]
    public class JsonRequestBuilderFactoryTest
    {
        [TestMethod]
        public void Execute_WhenBuilderFound_ExpectNonDefaultBuilderReturned()
        {
            //// Arrange
            //var mockParameters = new Mock<IParameters>();
            //var mockHandler = new Mock<IRequestHandler>();
            //mockHandler.Setup(m => m.CanHandle(mockParameters.Object));
            //// Act
            //RequestHandlerFactory target = new RequestHandlerFactory(mockHandler.Object);
            //var actual = target.Execute(mockParameters.Object);

            //// Assert
            //actual.Should().Equal(mockBuilder.Object);
        }

        [TestMethod]
        public void GetHandler_WhenBuilderNotFound_ExpectDefaultBuilderReturned()
        {
            // Arrange
            //Mock<IJsonRequestBuilder> mockDefaultBuilder = new Mock<IJsonRequestBuilder>();
            //Mock<IJsonRequestBuilder> mockBuilder = new Mock<IJsonRequestBuilder>();

            //Mock<IParameters> mockParameters = new Mock<IParameters>();

            //// Act
            //JsonRequestBuilderFactory target = new JsonRequestBuilderFactory(mockDefaultBuilder.Object, mockBuilder.Object);
            //var actual = target.GetBuilder(mockParameters.Object);

            //// Assert
            //actual.Should().Equal(mockDefaultBuilder.Object);
        }

        [TestMethod, ExceptionExpected(typeof(ArgumentNullException), "parameters")]
        public void GetHandler_WhenNullPassed_ExpectException()
        {
            // Arrange
            //Mock<IJsonRequestBuilder> mockDefaultBuilder = new Mock<IJsonRequestBuilder>();
            //Mock<IJsonRequestBuilder> mockBuilder = new Mock<IJsonRequestBuilder>();

            //// Act
            //JsonRequestBuilderFactory target = new JsonRequestBuilderFactory(mockDefaultBuilder.Object, mockBuilder.Object);
            //target.GetBuilder(null);

            // Assert
        }


        [TestMethod]
        public void GetDefaultBuilders_WhenCalled_ExpectNoException()
        {
            //var builders = JsonRequestBuilderFactory.GetDefaultBuilders();

            //builders.Should().Be.AssignableFrom(typeof(IJsonRequestBuilderFactory));
        }
    }
}
