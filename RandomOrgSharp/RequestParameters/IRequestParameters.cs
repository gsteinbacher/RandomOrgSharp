using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp.RequestParameters
{
    public interface IRequestParameters
    {
        int Id { get; }

        JObject CreateJsonRequest();
    }
}
