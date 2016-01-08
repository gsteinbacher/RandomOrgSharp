using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IErrorHandler : IResponseHandler
    {
        int Code { get; }
        string Message { get; }

        bool HasError(JObject json);
    }
}