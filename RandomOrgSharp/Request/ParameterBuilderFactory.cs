using System.Collections.Generic;
using System.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public class ParameterBuilderFactory : IParameterBuilderFactory
    {
        private readonly IList<IRequestBuilder> _builders;
        private readonly IRequestBuilder _defaultBuilder;

        public ParameterBuilderFactory(IRequestBuilder defaultBuilder, params IRequestBuilder[] builders)
        {
            _defaultBuilder = defaultBuilder;
            _builders = builders.ToList();
        }

        public IRequestBuilder GetBuilder(IParameters parameters)
        {
            return _builders.FirstOrDefault(m => m.CanHandle(parameters)) ?? _defaultBuilder;
        }
    }
}