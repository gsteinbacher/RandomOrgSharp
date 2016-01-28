using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Response;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Response
{
    [TestClass]
    public class UsageResponseInfoTest
    {
        [TestMethod]
        public void Constructor_WhenCalled_ExpectAllValuesSet()
        {
            const string version = "2.0";
            const StatusType status = StatusType.Running;
            DateTime creationTime = new DateTime(2016, 2, 1);
            const int bitsLeft = 200;
            const int requestsLeft = 300;
            const int totalBits = 100;
            const int totalRequests = 2;
            const int id = 400;

            UsageResponseInfo target = new UsageResponseInfo(version, status, creationTime, bitsLeft, requestsLeft, totalBits, totalRequests, id);

            target.Version.Should().Equal(version);
            target.Status.Should().Equal(status);
            target.CreationTime.Should().Equal(creationTime);
            target.BitsLeft.Should().Equal(bitsLeft);
            target.RequestsLeft.Should().Equal(requestsLeft);
            target.TotalBits.Should().Equal(totalBits);
            target.TotalRequests.Should().Equal(totalRequests);
            target.Id.Should().Equal(id);
            target.AdvisoryDelay.Should().Equal(0);
        }
    }
}
