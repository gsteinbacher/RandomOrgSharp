using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp
{
    public class RandomOrgServiceEmulator : IRandomOrgService
    {
        public JObject SendRequest(JObject jsonRequest)
        {
            throw new NotImplementedException();
        }

        public Task<JObject> SendRequestAsync(JObject jsonRequest)
        {
            throw new NotImplementedException();
        }
    }
}
