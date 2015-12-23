using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public class GuassianJsonParameterBuilder : IRequestBuilder
    {
        public JObject Create(IParameters parameters)
        {
            var guassianParameters = parameters as GuassianParameters;
            if (guassianParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "GuassianParameters"));

            var jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, guassianParameters.NumberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_MEAN_PARAMETER_NAME, guassianParameters.Mean),
                new JProperty(RandomOrgConstants.JSON_STANDARD_DEVIATION_PARAMETER_NAME, guassianParameters.StandardDeviation),
                new JProperty(RandomOrgConstants.JSON_SIGNIFICANT_DIGITS_PARAMETER_NAME, guassianParameters.SignificantDigits)
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
            return parameters.MethodType == MethodType.Gaussian;
        }
    }
}
