using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Obacher.RandomOrgSharp.Core.Service
{
    public class BitArrayConverter
    {
        private readonly BitArrayEnumerator _enumerator;

        public BitArrayConverter(BitArray bits)
        {
            _enumerator = new BitArrayEnumerator(bits);
        }

        public IEnumerable<int> GetIntegers(int numberOfItems, int minValue, int maxValue)
        {
            var maxBitCount = Math.Floor((Math.Log(maxValue) / Math.Log(2)) + 1);

            int count = 0;
            int rtnVal = 0;
            int bitCount = 0;
            foreach (var n in _enumerator)
            {
                if (bitCount > maxBitCount)
                {
                    bitCount = 0;
                    count++;
                    if (count > numberOfItems)
                        yield break;

                    if (rtnVal > minValue)
                        yield return rtnVal%maxValue;

                    yield return rtnVal;

                    rtnVal = 0;
                }

                rtnVal = (rtnVal << 1) ^ (n ? 1 : 0);
                bitCount++;
            }

            yield return rtnVal;
        }

        //public Guid GetGuid()
        //{
        //    //if (index + 16 > _length)
        //    //    throw new IndexOutOfRangeException();

        //    //var bytes = new byte[4];
        //    //_bits.CopyTo(bytes, index);
        //    //index += 4;

        //    byte[] rtnVal = 0;
        //    foreach (var n in _enumerator)
        //    {
        //        for (int i = 0; i < 3; i++)
        //            rtnVal = rtnVal >> (n ? 1 : 0);

        //        yield return rtnVal;
        //    }

        //    Guid returnValue = new Guid(bytes);
        //    return returnValue;
        //}

    }
}