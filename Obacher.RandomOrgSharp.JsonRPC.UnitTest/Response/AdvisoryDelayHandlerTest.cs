using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.Framework.Common.SystemWrapper.Interface;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Response;
using Obacher.UnitTest.Tools;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.JsonRPC.UnitTest.Response
{
    [TestClass]
    public class AdvisoryDelayHandlerTest
    {
        [TestMethod]
        public void Process_WhenAdvisoryIsZero_ShouldReturnTrue()
        {
            // Arrange

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler();
            var actual = target.Process(null);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void Process_WhenAdvisoryIsNotZero_ShouldReturnTrue()
        {
            // Arrange
            const long delayTime = 100;
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(new DateTimeWrap(2016, 2, 1));
            long advisoryDelay = new DateTime(2016, 2, 1).Ticks - delayTime;

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_advisoryDelay", advisoryDelay);

            Stopwatch stopwatch = Stopwatch.StartNew();
            var actual = target.Process(null);
            stopwatch.Stop();

            // Assert
            actual.Should().Be.True();
            stopwatch.ElapsedMilliseconds.Should().Be.GreaterThan(delayTime);
        }

        [TestMethod]
        public void Process_WhenWaitTimeLessThanZero_ShouldReturnQuickly()
        {
            // Arrange
            const long delayTime = 100;
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(new DateTimeWrap(2016, 2, 1));
            long advisoryDelay = new DateTime(2016, 2, 1).Ticks + delayTime;

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler(dateTimeMock.Object);
            UnitTestHelper.SetPrivateProperty(target, "_advisoryDelay", advisoryDelay);

            Stopwatch stopwatch = Stopwatch.StartNew();
            var actual = target.Process(null);
            stopwatch.Stop();

            // Assert
            actual.Should().Be.True();
            stopwatch.ElapsedMilliseconds.Should().Be.LessThan(delayTime);
        }

        [TestMethod]
        public void Handle_WhenAdvisoryDelayGreaterThanZero_ShouldSetAdvisoryDelay()
        {
            // Arrange
            const int delayTime = 100;
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.UtcNow).Returns(new DateTimeWrap(2016, 2, 1));
            long expected = new DateTime(2016, 2, 1).Ticks + delayTime;

            var input =
                new JObject(
                    new JProperty(
                        "result",
                        new JObject(
                            new JProperty(JsonRpcConstants.ADVISORY_DELAY_PARAMETER_NAME, delayTime)
                         )
                     )
                 );

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler(dateTimeMock.Object);
            var actual = target.Handle(null, input.ToString());
            long advisoryDelay = UnitTestHelper.GetPrivateProperty<long>(target, "_advisoryDelay");

            // Assert
            actual.Should().Be.True();
            advisoryDelay.Should().Equal(expected);
        }

        [TestMethod]
        public void Handle_WhenAdvisoryDelayIsZero_ShouldSetAdvisoryDelayToZero()
        {
            // Arrange
            const long expected = 0;

            var input = new JObject(
                new JProperty("result",
                    new JObject(
                        new JProperty(JsonRpcConstants.ADVISORY_DELAY_PARAMETER_NAME, expected)
                        ))
                );

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler();
            var actual = target.Handle(null, input.ToString());
            long advisoryDelay = UnitTestHelper.GetPrivateProperty<long>(target, "_advisoryDelay");

            // Assert
            actual.Should().Be.True();
            advisoryDelay.Should().Equal(expected);
        }

        [TestMethod]
        public void CanProcess_WhenCalled_ShouldAlwaysReturnTrue()
        {
            // Arrange

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler(null);
            var actual = target.CanProcess(null);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenBlobMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Blob);

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler();
            var actual = target.CanHandle(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenDecimalMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Decimal);

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler();
            var actual = target.CanHandle(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandlee_WhenGaussianMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Gaussian);

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler();
            var actual = target.CanHandle(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenIntegerMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Integer);

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler();
            var actual = target.CanHandle(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenStringMethodType_ShouldReturnTrue()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.String);

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler();
            var actual = target.CanHandle(parametersMock.Object);

            // Assert
            actual.Should().Be.True();
        }

        [TestMethod]
        public void CanHandle_WhenUsageMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.Usage);

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler();
            var actual = target.CanHandle(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }

        [TestMethod]
        public void CanHandlee_WhenVerifySignatureMethodType_ShouldReturnFalse()
        {
            // Arrange
            Mock<IParameters> parametersMock = new Mock<IParameters>();
            parametersMock.Setup(m => m.MethodType).Returns(MethodType.VerifySignature);

            // Act
            AdvisoryDelayHandler target = new AdvisoryDelayHandler();
            var actual = target.CanHandle(parametersMock.Object);

            // Assert
            actual.Should().Be.False();
        }
    }
}