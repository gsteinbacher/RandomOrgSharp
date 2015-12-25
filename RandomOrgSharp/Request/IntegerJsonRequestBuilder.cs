using System;  // 
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public class IntegerJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Create(IParameters parameters)
        {
            var integerParameters = parameters as IntegerParameters;
            if (integerParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "IntegerParameters"));

            var jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, integerParameters.NumberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_MINIMUM_VALUE_PARAMETER_NAME, integerParameters.MinimumValue),
                new JProperty(RandomOrgConstants.JSON_MAXIMUM_VALUE_PARAMETER_NAME, integerParameters.MaximumValue),
                new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, integerParameters.AllowDuplicates),
                new JProperty(RandomOrgConstants.JSON_BASE_NUMBER_PARAMETER_NAME, 10)  // Always do Base10 because the client can convert it to whatever base needed once it is returned.
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
            return parameters.MethodType == MethodType.Integer;
        }
    }
}
