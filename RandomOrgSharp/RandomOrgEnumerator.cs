using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obacher.RandomOrgSharp
{
    public class RandomOrgEnumerator<T> : IEnumerable<T>, IEnumerator<T>
    {
        private IList<T> _list;
        private bool _isDisposed;

        public RandomOrgEnumerator(IList<T> list)
        {
            _list = list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            return _list.GetEnumerator().MoveNext();
        }

        public void Reset()
        {
            _list.GetEnumerator().Reset();
        }

        public T Current => _list.GetEnumerator().Current;

        object IEnumerator.Current => Current;

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
                {
                }

                _isDisposed = true;
            }
        }
    }
}
