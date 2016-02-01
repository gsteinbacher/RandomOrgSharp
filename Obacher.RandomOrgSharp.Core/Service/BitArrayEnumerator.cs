using System;
using System.Collections;
using System.Collections.Generic;

namespace Obacher.RandomOrgSharp.Core.Service
{
    public class BitArrayEnumerator : IEnumerable<bool>
    {
        private readonly BitArray _bits;

        public BitArrayEnumerator(BitArray bits)
        {
            _bits = bits;
        }


        public IEnumerator<bool> GetEnumerator()
        {
            return new BitArrayIterator(_bits);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private class BitArrayIterator : IEnumerator<bool>
        {
            private readonly BitArray _bits;
            private readonly int _length;

            private int _currentStartIndex;
            private int _currentPosition;
            private bool _wrapped;

            public BitArrayIterator(BitArray bits)
            {
                _bits = bits;
                _length = bits.Length;

                _currentPosition = -1;
                _currentStartIndex = 0;
            }


            public bool MoveNext()
            {
                _currentPosition++;

                if (!_wrapped)
                {
                    if (_currentPosition >= _length)
                    {
                        if (_currentStartIndex == 0)
                        {
                            _currentStartIndex++;
                            _currentPosition = 1;
                        }
                        else
                        {
                            _currentPosition = 0;
                            _wrapped = true;
                        }
                    }
                }
                else
                {
                    if (_currentPosition >= _currentStartIndex)
                    {
                        _wrapped = false;
                        _currentPosition = ++_currentStartIndex;
                        if (_currentStartIndex >= _length)
                            return false;
                    }
                }


                return true;
            }

            public void Reset()
            {
                _currentPosition = -1;
                _currentStartIndex = 0;
            }

            public bool Current => _bits.Get(_currentPosition);
            object IEnumerator.Current => Current;

            public void Dispose()
            {

            }
        }
    }
}