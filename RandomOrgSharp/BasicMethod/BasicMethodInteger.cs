using System.Collections.Generic;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp.RequestParameters;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class BasicMethodInteger
    {
        private readonly IBasicMethod<int> _basicMethod;

        public BasicMethodInteger(IRandomOrgService service)
        {
            _basicMethod = new BasicMethod<int>(service);
        }

        public BasicMethodInteger(IBasicMethod<int> basicMethod)
        {
            _basicMethod = basicMethod;
        }

        public IEnumerable<int> Execute(IRequestParameters requestParameters)
        {
            var response = _basicMethod.Execute(requestParameters);
            return response;
        }


        public async Task<IEnumerable<int>> ExecuteAsync(IRequestParameters requestParameters)
        {
            var response = await _basicMethod.ExecuteAsync(requestParameters);
            return response;
        }
    }
}
