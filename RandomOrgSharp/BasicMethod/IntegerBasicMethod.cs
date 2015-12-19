using System.Collections.Generic;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp.RequestParameters;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class IntegerBasicMethod
    {
        private readonly IBasicMethod<int> _basicMethod;

        public IntegerBasicMethod(IRandomOrgService service, IMethodCallManager methodCallManager)
        {
            _basicMethod = new BasicMethod<int>(service, methodCallManager);
        }

        public IntegerBasicMethod(IBasicMethod<int> basicMethod)
        {
            _basicMethod = basicMethod;
        }

        public IEnumerable<int> Execute(IRequestParameters requestParameters)
        {
            var response = _basicMethod.Generate(requestParameters);
            return response;
        }


        public async Task<IEnumerable<int>> ExecuteAsync(IRequestParameters requestParameters)
        {
            var response = await _basicMethod.GenerateAsync(requestParameters);
            return response;
        }
    }
}
