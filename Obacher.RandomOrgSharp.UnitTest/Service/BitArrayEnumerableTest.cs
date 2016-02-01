using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core.Service;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Service
{
    [TestClass]
    public class BitArrayEnumerableTest
    {
        [TestMethod]
        public void WhenCalled_ExpectReturnValue()
        {
            string expected = "00001111 00011110 00111100 01111000 11110000 11100001 11000011 10000111";
            bool[] bools = { false, false, false, false, true, true, true, true };
            BitArray data = new BitArray(bools);

            var target = new BitArrayEnumerator(data);
            string actual = target.Aggregate("", (current, b) => current + (b ? "1" : "0"));

            actual.Should().Equal(expected.Replace(" ",""));
        }
    }
}
