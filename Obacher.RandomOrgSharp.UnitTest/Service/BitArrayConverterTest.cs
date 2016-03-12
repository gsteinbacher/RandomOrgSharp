using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core.Service;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Service
{
    [TestClass]
    public class BitArrayConverterTest
    {
        [TestMethod, Ignore]
        public void GetInteger_WhenCalled_ExpectRandomOrderOfIntegers()
        {
            List<int> expected = Enumerable.Range(0, 64).ToList();
            bool[] bools = { false, false, false, false, true, true, true, true };
            BitArray data = new BitArray(bools);

            var target = new BitArrayConverter(data);
            IEnumerable<int> actual = target.GetIntegers(10, 1, 100).ToList();

            CollectionAssert.AreEqual(expected, actual.ToList());
        }
    }
}
