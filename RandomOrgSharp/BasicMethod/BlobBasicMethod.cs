using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Win32;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

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

        public IBasicMethodResponse<string> Execute(int numberOfItemsToReturn, int size, BlobFormat format)
        {
            var parameters = BlobParameters.Set(numberOfItemsToReturn, size, format);
            var parameterBuilder = new BlobJsonParameterBuilder();
            var requestBuilder = new JsonRequestBuilder(parameterBuilder);

            var responseParser = new BasicMethodResponseParser<string>();

            var response = _basicMethod.Generate(requestBuilder, responseParser, parameters);
            return response;
        }


        public async Task<IBasicMethodResponse<string>> ExecuteAsync(int numberOfItemsToReturn, int size, BlobFormat format)
        {
            var parameters = BlobParameters.Set(numberOfItemsToReturn, size, format);
            var parameterBuilder = new BlobJsonParameterBuilder(parameters);
            var requestBuilder = new JsonRequestBuilder(parameterBuilder);

            var responseParser = new BasicMethodResponseParser<string>();

            var response = await _basicMethod.GenerateAsync(requestBuilder, responseParser, parameters);
            return response;
        }
    }
}
