using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
{
    [TestClass]
    public class UsageParametersTest
    {
        [TestMethod]
        public void WhenCalled_ExpectPropertiesSetProperly()
        {
            // Arrange
                // Arrange
                const MethodType expectedMethodType = MethodType.Usage;
                const bool expectedVerifyOriginator = false;
                // Act
                var parameters = UsageParameters.Create();

                // Arrange
                parameters.MethodType.Should().Equal(expectedMethodType);
                parameters.VerifyOriginator.Should().Equal(expectedVerifyOriginator);
        }
    }
}
