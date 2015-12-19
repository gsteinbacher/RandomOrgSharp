using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp.RequestParameters;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class UUIDBasicMethod
    {
        private readonly IBasicMethod<string> _basicMethod;

        public UUIDBasicMethod(IRandomOrgService service, IMethodCallManager methodCallManager)
        {
            _basicMethod = new BasicMethod<string>(service, methodCallManager);
        }

        public UUIDBasicMethod(IBasicMethod<string> basicMethod)
        {
            _basicMethod = basicMethod;
        }

        public IEnumerable<Guid> Execute(IRequestParameters requestParameters)
        {
            var response = _basicMethod.Generate(requestParameters);

            var guids = Array.ConvertAll(response.ToArray(), id => new Guid(id));
            return guids;
        }


        public async Task<IEnumerable<Guid>> ExecuteAsync(IRequestParameters requestParameters)
        {
            var response = await _basicMethod.GenerateAsync(requestParameters);

            var guids = Array.ConvertAll(response.ToArray(), id => new Guid(id));
            return guids;
        }
    }
}
