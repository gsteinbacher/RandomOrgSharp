using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.JsonRPC.Request
{
    public class GuassianJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Build(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var guassianParameters = parameters as GuassianParameters;
            if (guassianParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "GuassianParameters"));

            var jsonParameters = new JObject(
                new JProperty(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, guassianParameters.NumberOfItemsToReturn),
                new JProperty(JsonRpcConstants.MEAN_PARAMETER_NAME, guassianParameters.Mean),
                new JProperty(JsonRpcConstants.STANDARD_DEVIATION_PARAMETER_NAME, guassianParameters.StandardDeviation),
                new JProperty(JsonRpcConstants.SIGNIFICANT_DIGITS_PARAMETER_NAME, guassianParameters.SignificantDigits)
            );

            return jsonParameters;
        }

        /// <summary>
        /// Identify this class as one that handles Guassian parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <returns>True if this class handles the specified parameters</returns>
        public bool CanHandle(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return parameters.MethodType == MethodType.Gaussian;
        }
    }
}
