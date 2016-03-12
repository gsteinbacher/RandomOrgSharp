using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core.Response;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Response
{
    [TestClass]
    public class DataResponseInfoTest
    {
        [TestMethod]
        public void Constructor_WhenCalled_ExpectAllValuesSet()
        {
            const string version = "2.0";
            IEnumerable<int> data = new List<int> { 1, 4, 6 };
            DateTime completionTime = new DateTime(2016, 2, 1);
            int bitsUsed = 100;
            int bitsLeft = 200;
            int requestsLeft = 300;
            int advisoryDelay = 2;
            int id = 400;

            DataResponseInfo<int> target = new DataResponseInfo<int>(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id);

            target.Version.Should().Equal(version);
            target.Data.Should().Equal(data);
            target.CompletionTime.Should().Equal(completionTime);
            target.BitsUsed.Should().Equal(bitsUsed);
            target.BitsLeft.Should().Equal(bitsLeft);
            target.RequestsLeft.Should().Equal(requestsLeft);
            target.AdvisoryDelay.Should().Equal(advisoryDelay);
            target.Id.Should().Equal(id);

        }
    }
}
