using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Method;
using Obacher.RandomOrgSharp.JsonRPC.Response;
using Should.Fluent;

namespace Obacher.RandomOrgSharp.Emulator.UnitTest
{
    [TestClass]
    public class RandomOrgHttpEmulatorTest
    {
        private AdvisoryDelayHandler _advisoryDelayHandler;

        [TestInitialize]
        public void InitializeTest()
        {
            _advisoryDelayHandler = new AdvisoryDelayHandler(new DateTimeWrap());
        }

        [TestCleanup]
        public void CleanupTest()
        {
            _advisoryDelayHandler = null;
        }
        [TestMethod]
        public void SendRequest_WhenIntegerCalledWithBase10_ExpectBase10IntegerValuesReturned()
        {
            // Arrange
            const int numberOfItemsReturned = 100;
            const int minValue = 1;
            const int maxValue = 1000000;

            RandomOrgApiEmulator service = new RandomOrgApiEmulator();

            // Act
            var target = new IntegerBasicMethod(_advisoryDelayHandler, service);
            var actual = target.GenerateIntegers(numberOfItemsReturned, minValue, maxValue);

            // Assert
            actual.Should().Not.Be.Null();
        }
    }
}
