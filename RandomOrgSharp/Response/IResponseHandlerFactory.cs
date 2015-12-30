using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public interface IResponseHandlerFactory
    {
        bool Execute(JObject json, IParameters parameters);
        IResponseHandler GetHandler(Type handlerType);
    }
}