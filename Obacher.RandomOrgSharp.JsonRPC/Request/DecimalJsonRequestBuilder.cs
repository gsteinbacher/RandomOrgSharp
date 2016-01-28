using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;

namespace Obacher.RandomOrgSharp.JsonRPC.Request
{
    public class DecimalJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Build(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var decimalParameters = parameters as DecimalParameters;
            if (decimalParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "DecimalParameters"));

            var jsonParameters = new JObject(
                new JProperty(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, decimalParameters.NumberOfItemsToReturn),
                new JProperty(JsonRpcConstants.DECIMAL_PLACES_PARAMETER_NAME, decimalParameters.NumberOfDecimalPlaces),
                new JProperty(JsonRpcConstants.REPLACEMENT_PARAMETER_NAME, decimalParameters.AllowDuplicates)
                );

            return jsonParameters;
        }

        /// <summary>
        /// Identify this class as one that handles Decimal parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <returns>True if this class handles the specified parameters</returns>
        public bool CanHandle(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return parameters.MethodType == MethodType.Decimal;
        }
    }
}
