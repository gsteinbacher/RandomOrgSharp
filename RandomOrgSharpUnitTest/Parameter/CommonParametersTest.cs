using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.UnitTest.Tools.Mocks;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Parameter
{
    [TestClass]
    public class CommonParametersTest
    {
        [TestMethod]
        public void WhenConstructorInitialized_ExpectPropertiesSetCorrectly()
        {
            // Arrange
            const int expectedId = 9999;
            const MethodType expectedMethod = MethodType.Integer;
            const bool expectedVerifyOriginator = true;

            RandomNumberGenerator.Instance = MockHelper.SetupIdMock(expectedId).Object;

            // Act
            var actual = new CommonParameters(expectedMethod, expectedVerifyOriginator);

            // Assert
            actual.MethodType.Should().Equal(expectedMethod);
            actual.VerifyOriginator.Should().Equal(expectedVerifyOriginator);
            actual.Id.Should().Equal(expectedId);
        }

        [TestMethod]
        public void GetMethodName_WhenCalled_ExpectCorrectMethodName()
        {
            // Arrange

            // Act
            var target = new CommonParameters(MethodType.Blob);
            var actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.BLOB_METHOD);

            target = new CommonParameters(MethodType.Blob, true);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.BLOB_SIGNED_METHOD);

            target = new CommonParameters(MethodType.Decimal);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.DECIMAL_METHOD);

            target = new CommonParameters(MethodType.Decimal, true);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.DECIMAL_SIGNED_METHOD);

            target = new CommonParameters(MethodType.Gaussian);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.GAUSSIAN_METHOD);

            target = new CommonParameters(MethodType.Gaussian, true);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.GAUSSIAN_SIGNED_METHOD);

            target = new CommonParameters(MethodType.Integer);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.INTEGER_METHOD);

            target = new CommonParameters(MethodType.Integer, true);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.INTEGER_SIGNED_METHOD);

            target = new CommonParameters(MethodType.String);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.STRING_METHOD);

            target = new CommonParameters(MethodType.String, true);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.STRING_SIGNED_METHOD);

            target = new CommonParameters(MethodType.Uuid);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.UUID_METHOD);

            target = new CommonParameters(MethodType.Uuid, true);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.UUID_SIGNED_METHOD);

            target = new CommonParameters(MethodType.Usage);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.USAGE_METHOD);

            target = new CommonParameters(MethodType.Usage, true);
            actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.USAGE_METHOD);
        }

        [TestMethod]
        public void GetMethodName_WhenNull_ExpectCorrectMethodName()
        {
            // Arrange
            // Act
            var target = new CommonParameters(MethodType.Blob);
            var actual = target.GetMethodName();
            actual.Should().Equal(RandomOrgConstants.BLOB_METHOD);
        }
    }
}

