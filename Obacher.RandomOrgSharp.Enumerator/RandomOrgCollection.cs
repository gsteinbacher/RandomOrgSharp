using System;
using System.Collections;
using System.Collections.Generic;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.JsonRPC.Response;

namespace Obacher.RandomOrgSharp.Enumerator
{
    public class RandomOrgEnumerator<T> :  IEnumerator<T>
    {
        private readonly IParameters _parameters;
        private IMethodCallBroker _methodCallBroker;
        private JsonResponseParserFactory _responseParser;

        public RandomOrgEnumerator(int numberOfItemsToReturn, IParameters parameters, IResponseParser responseParser)
        {
            _parameters = parameters;
        }

        public void Initialize(IRequestBuilder requestBuilder, IPrecedingRequestCommandFactory precedingRequestCommandFactory, IResponseHandlerFactory responseHandlerFactory, JsonResponseParserFactory responseParser, IRandomService randomService)
        {
            _methodCallBroker = new MethodCallBroker(requestBuilder, randomService, precedingRequestCommandFactory, responseHandlerFactory);
        }

        private IEnumerator<T> _listEnumerator;

        public bool MoveNext()
        {
            var data = _methodCallBroker.Generate(_parameters);
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public T Current { get; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

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
