using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.RequestParameters;

namespace Obacher.RandomOrgSharp
{
    public interface IMethodCallManager
    {
        void Delay();
        void SetAdvisoryDelay(int advisoryDelay);
        void CanSendRequest();
        void ThrowExceptionOnError(JObject jsonResponse);
        void VerifyResponse(IRequestParameters requestParameters, BasicMethodResponse response);
    }
}