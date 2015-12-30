using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class ParseResponse : IResponseHandler
    {
        public bool Process(JObject json, IParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool IsHandlerOfType(string handlerType)
        {
            //return string.Equals(handlerType, RandomOrgConstants.HANDLER_TYPE_ERROR, StringComparison.InvariantCultureIgnoreCase);
            return false;
        }
    }
}