using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.JsonRPC.Method;
using Obacher.RandomOrgSharp.JsonRPC.Response;

namespace Obacher.RandomOrgSharp.Enumerator
{
    public class BlobEnumerator : IEnumerator<string>, IEnumerable<string>
    {
        private int _numberOfItemsToReturn;
        private int _size;
        private BlobFormat _format;
        private int _totalNumberOfItemsReturned = 0;
        private int _numberOfItemsToReturnPerCall;


        private BlobBasicMethod _method;
        private IList<string> _data = new List<string>();
        private IEnumerator<string> _dataEnumerator;

        private bool _isDisposed;

        public BlobEnumerator(int size, BlobFormat format = BlobFormat.Base64, int numberOfItemsToReturn = 0)
        {
            _size = size;
            _format = format;

            _numberOfItemsToReturn = numberOfItemsToReturn;
            _numberOfItemsToReturnPerCall = _numberOfItemsToReturn == 0
                ? BlobParameters.MaxItemsAllowed
                : Math.Min(BlobParameters.MaxItemsAllowed, numberOfItemsToReturn);
        }

        public void Initialize(AdvisoryDelayHandler advisoryDelayHandler, IRandomService randomService = null, bool verifyOriginator = false)
        {
            _method = verifyOriginator ?
                new BlobSignedMethod(advisoryDelayHandler, randomService) :
                new BlobBasicMethod(advisoryDelayHandler, randomService);
        }


        public IEnumerator<string> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            bool result = false;
            if (_numberOfItemsToReturn == 0 || _numberOfItemsToReturn < _totalNumberOfItemsReturned)
            {
                if (_dataEnumerator == null)
                    FillData();

                if (_dataEnumerator != null)
                {
                    result = _dataEnumerator.MoveNext();
                    if (result)
                    {
                        Current = _dataEnumerator.Current;
                    }
                    else
                    {
                        FillData();

                        result = _dataEnumerator.MoveNext();
                        if (result)
                            Current = _dataEnumerator.Current;
                    }
                }
            }

            return result;
        }

        private void FillData()
        {
            int numberOfItemsToReturn = _numberOfItemsToReturn == 0
                ? _numberOfItemsToReturnPerCall
                : Math.Min(_numberOfItemsToReturn - _totalNumberOfItemsReturned, _numberOfItemsToReturnPerCall);

            _data = _method.GenerateBlobs(numberOfItemsToReturn, _size, _format)?.ToList();
            if (_data == null)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.NO_DATA_RETURNED));

            _totalNumberOfItemsReturned += _data.Count;
            _dataEnumerator = _data.GetEnumerator();
        }

        public void Reset()
        {
            throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.CANNOT_CALL_RESET));
        }

        public string Current { get; private set; }

        object IEnumerator.Current => Current;

        public int Count => _totalNumberOfItemsReturned;

        #region Implementation of Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                    _dataEnumerator?.Dispose();

                _isDisposed = true;
            }
        }

        #endregion
    }
}