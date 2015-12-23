﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class DecimalBasicMethod
    {
        private readonly IBasicMethod<decimal> _basicMethod;

        public DecimalBasicMethod(IRandomOrgService service, IMethodCallManager methodCallManager)
        {
            _basicMethod = new BasicMethod<decimal>(service, methodCallManager);
        }

        public DecimalBasicMethod(IBasicMethod<decimal> basicMethod)
        {
            _basicMethod = basicMethod;
        }

        public IEnumerable<decimal> Execute(IRequestParameters requestParameters)
        {
            var response = _basicMethod.Generate(requestParameters);
            return response;
        }


        public async Task<IEnumerable<decimal>> ExecuteAsync(IRequestParameters requestParameters)
        {
            var response = await _basicMethod.GenerateAsync(requestParameters);
            return response;
        }
    }
}
