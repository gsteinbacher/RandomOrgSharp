using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.Error
{
    public interface IErrorHandler : IResponseHandler
    {
        int Code { get; }
        string Message { get; }

        bool HasError(JObject json);
    }
}