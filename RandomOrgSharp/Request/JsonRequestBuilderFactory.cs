using System.Collections.Generic;
using System.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public class JsonRequestBuilderFactory : IJsonRequestBuilderFactory
    {
        private readonly IList<IJsonRequestBuilder> _builders;
        private readonly IJsonRequestBuilder _defaultBuilder;

        /// <summary>
        /// Instantiate the factory to handle creation of the parameters that are specific to each method call.  
        /// The common parameters used for all api calls is created in JsonRequestBuilder.
        /// If no builders are specified then the default json builders are used.
        /// </summary>
        /// <param name="defaultBuilder">The default builder if a specific builder is not found</param>
        /// <param name="builders">List of builders to use</param>
        public JsonRequestBuilderFactory(IJsonRequestBuilder defaultBuilder, params IJsonRequestBuilder[] builders)
        {
            _defaultBuilder = defaultBuilder;
            _builders = builders.ToList();
        }

        public IJsonRequestBuilder GetBuilder(IParameters parameters)
        {
            return _builders.FirstOrDefault(m => m.CanHandle(parameters)) ?? _defaultBuilder;
        }
    }
}