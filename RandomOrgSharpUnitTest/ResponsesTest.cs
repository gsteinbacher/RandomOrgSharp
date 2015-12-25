using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest
{
    [TestClass]
    public class ResponsesTest
    {
        [TestMethod]
        public void WhenBasicMethodResponseParseCalled_ExpectBasicMethodResponsePropertiesPopulated()
        {
            List<int> expected = new List<int> { 1, 5, 4, 6, 6, 4 };
            DateTime utcNow = DateTime.UtcNow;
            var input = JObject.Parse(@"{
    jsonrpc: '2.0',
    result: {
                random: {
                    data: [" + String.Join(",", expected) + @"],
            completionTime: '" + utcNow + @"'
        },
        bitsUsed: 16,
        bitsLeft: 199984,
        requestsLeft: 9999,
        advisoryDelay: 0
    },
    id: 42
}");
            //var actual = BasicMethodResponse.Parse(input);

            //"2.0".Should().Equal(actual.Version);
            //expected.Should().Equal(actual.Data.Values<int>());
            //utcNow.Date.Should().Equal(actual.CompletionTime.Date);
            //16.Should().Equal(actual.BitsUsed);
            //199984.Should().Equal(actual.BitsLeft);
            //9999.Should().Equal(actual.RequestsLeft);
            //0.Should().Equal(actual.AdvisoryDelay);
            //42.Should().Equal(actual.Id);
        }
    }
}
