using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.JsonRPC.Request
{
    public class StringJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Build(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var stringParameters = parameters as StringParameters;
            if (stringParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "StringParameters"));

            var jsonParameters = new JObject(
                new JProperty(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, stringParameters.NumberOfItemsToReturn),
                new JProperty(JsonRpcConstants.LENGTH_PARAMETER_NAME, stringParameters.Length),
                new JProperty(JsonRpcConstants.CHARACTERS_ALLOWED_PARAMETER_NAME, stringParameters.CharactersAllowed),
                new JProperty(JsonRpcConstants.REPLACEMENT_PARAMETER_NAME, stringParameters.AllowDuplicates)
                );

            return jsonParameters;
        }

        /// <summary>
        /// Identify this class as one that handles String parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <returns>True if this class handles the specified parameters</returns>
        public bool CanHandle(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return parameters.MethodType == MethodType.String;
        }
    }
}
