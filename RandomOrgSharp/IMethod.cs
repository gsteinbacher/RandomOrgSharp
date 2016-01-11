using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.Core
{
    public interface IMethod
    {
        IRequestBuilder CreateRequestBuilder();
        void ParseResponse(string response);
        IResponseInfo GetResponseInfo();
    }
}