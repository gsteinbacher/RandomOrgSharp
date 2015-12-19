﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp.RequestParameters;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class StringBasicMethod
    {
        private readonly IBasicMethod<string> _basicMethod;

        public StringBasicMethod(IRandomOrgService service, IMethodCallManager methodCallManager)
        {
            _basicMethod = new BasicMethod<string>(service, methodCallManager);
        }

        public StringBasicMethod(IBasicMethod<string> basicMethod)
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
