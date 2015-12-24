using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public class DecimalJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Create(IParameters parameters)
        {
            var decimalParameters = parameters as DecimalParameters;
            if (decimalParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "DecimalParameters"));

            var jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, decimalParameters.NumberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_DECIMAL_PLACES_PARAMETER_NAME, decimalParameters.NumberOfDecimalPlaces),
                new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, decimalParameters.AllowDuplicates)
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
            return parameters.MethodType == MethodType.Decimal;
        }
    }
}
