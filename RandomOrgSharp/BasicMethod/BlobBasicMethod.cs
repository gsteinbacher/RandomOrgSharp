using System.Collections.Generic;
using System.Threading.Tasks;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class BlobBasicMethod
    {
        private readonly IBasicMethod<string> _basicMethod;

        public BlobBasicMethod(IRandomOrgService service, IMethodCallManager methodCallManager)
        {
            _basicMethod = new BasicMethod<string>(service, methodCallManager);
        }

        public BlobBasicMethod(IBasicMethod<string> basicMethod)
        {
            _basicMethod = basicMethod;
        }

        public IEnumerable<string> Execute(IRequestParameters requestParameters)
        {
            var response = _basicMethod.Generate(requestParameters);
            return response;
        }


        public async Task<IEnumerable<string>> ExecuteAsync(IRequestParameters requestParameters)
        {
            var response = await _basicMethod.GenerateAsync(requestParameters);
            return response;
        }
    }
}
