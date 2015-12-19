using System.Collections.Generic;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp.RequestParameters;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class GuassianBasicMethod
    {
        private readonly IBasicMethod<double> _basicMethod;

        public GuassianBasicMethod(IRandomOrgService service, IMethodCallManager methodCallManager)
        {
            _basicMethod = new BasicMethod<double>(service, methodCallManager);
        }

        public GuassianBasicMethod(IBasicMethod<double> basicMethod)
        {
            _basicMethod = basicMethod;
        }

        public IEnumerable<double> Execute(IRequestParameters requestParameters)
        {
            var response = _basicMethod.Generate(requestParameters);
            return response;
        }


        public async Task<IEnumerable<double>> ExecuteAsync(IRequestParameters requestParameters)
        {
            var response = await _basicMethod.GenerateAsync(requestParameters);
            return response;
        }
    }
}

