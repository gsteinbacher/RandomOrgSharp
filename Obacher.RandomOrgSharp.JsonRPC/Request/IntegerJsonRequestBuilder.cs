using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.JsonRPC.Request
{
    public class IntegerJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Build(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var integerParameters = parameters as IntegerParameters;
            if (integerParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "IntegerParameters"));

            var jsonParameters = new JObject(
                new JProperty(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, integerParameters.NumberOfItemsToReturn),
                new JProperty(JsonRpcConstants.MINIMUM_VALUE_PARAMETER_NAME, integerParameters.MinimumValue),
                new JProperty(JsonRpcConstants.MAXIMUM_VALUE_PARAMETER_NAME, integerParameters.MaximumValue),
                new JProperty(JsonRpcConstants.REPLACEMENT_PARAMETER_NAME, integerParameters.AllowDuplicates),
                new JProperty(JsonRpcConstants.BASE_NUMBER_PARAMETER_NAME, 10)  // Always do Base10 because the client can convert it to whatever base needed once it is returned.
            );

            return jsonParameters;
        }

        /// <summary>
        /// Identify this class as one that handles Integer parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <returns>True if this class handles the specified parameters</returns>
        public bool CanHandle(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return parameters.MethodType == MethodType.Integer;
        }
    }
}
