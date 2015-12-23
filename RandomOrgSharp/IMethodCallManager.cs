using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp
{
    public interface IMethodCallManager
    {
        void Delay();
        void SetAdvisoryDelay(int advisoryDelay);
        void CanSendRequest();
        void ThrowExceptionOnError(JObject jsonResponse);
        void VerifyResponse(IParameters requestParameters, IResponse response);
    }
}