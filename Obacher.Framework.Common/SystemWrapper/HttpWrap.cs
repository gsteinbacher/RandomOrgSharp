using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Obacher.Framework.Common.SystemWrapper
{
    public interface IHttpWebRequest
    {
        // expose the members you need
        string Method { get; set; }
        int Timeout { get; set; }
        int ReadWriteTimeout { get; set; }
        string ContentType { get; set; }

        IHttpWebResponse GetResponse();
        Task<IHttpWebResponse> GetResponseAsync();
        Stream GetRequestStream();
    }

    public interface IHttpWebResponse : IDisposable
    {
        Stream GetResponseStream();
    }

    public interface IHttpWebRequestFactory
    {
        IHttpWebRequest Create(string uri);
    }

    public class HttpWebRequestFactory : IHttpWebRequestFactory
    {
        public IHttpWebRequest Create(string uri)
        {
            return new WrapHttpWebRequest((HttpWebRequest)WebRequest.Create(uri));
        }
    }

    public class WrapHttpWebRequest : IHttpWebRequest
    {
        private readonly HttpWebRequest _request;

        public WrapHttpWebRequest(HttpWebRequest request)
        {
            _request = request;
        }

        public string Method
        {
            get { return _request.Method; }
            set { _request.Method = value; }
        }

        public int Timeout
        {
            get { return _request.Timeout; }
            set { _request.Timeout = value; }
        }
        public int ReadWriteTimeout
        {
            get { return _request.ReadWriteTimeout; }
            set { _request.ReadWriteTimeout = value; }
        }
        public string ContentType
        {
            get { return _request.ContentType; }
            set { _request.ContentType = value; }
        }

        public IHttpWebResponse GetResponse()
        {
            return new WrapHttpWebResponse((HttpWebResponse)_request.GetResponse());
        }

        public async Task<IHttpWebResponse> GetResponseAsync()
        {
            WebResponse response = await _request.GetResponseAsync();
            return new WrapHttpWebResponse((HttpWebResponse)response);
        }

        public Stream GetRequestStream()
        {
            return _request.GetRequestStream();
        }
    }

    public class WrapHttpWebResponse : IHttpWebResponse
    {
        private WebResponse _response;

        public WrapHttpWebResponse(HttpWebResponse response)
        {
            _response = response;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_response != null)
                {
                    ((IDisposable)_response).Dispose();
                    _response = null;
                }
            }
        }

        public Stream GetResponseStream()
        {
            return _response.GetResponseStream();
        }
    }
}