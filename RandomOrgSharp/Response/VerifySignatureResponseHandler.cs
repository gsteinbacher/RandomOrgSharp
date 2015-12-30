using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class VerifySignatureResponseHandler : IResponseHandler
    {
        public bool Process(JObject json, IParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool IsHandlerOfType(string handlerType)
        {
            return string.Equals(handlerType, RandomOrgConstants.VERIFY_SIGNATURE_METHOD, StringComparison.InvariantCultureIgnoreCase);

        }
    }
}