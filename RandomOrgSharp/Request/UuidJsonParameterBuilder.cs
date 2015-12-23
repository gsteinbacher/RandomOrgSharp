using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public class UuidJsonParameterBuilder : IRequestBuilder
    {
        public JObject Create(IParameters parameters)
        {
            var uuidParameters = parameters as UuidParameters;
            if (uuidParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "UuidParameters"));

            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, uuidParameters.NumberOfItemsToReturn)
            );

            return jsonParameters;
        }

        /// <summary>
        /// Identify this class as one that handles UUID parameters
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <returns>True if this class handles the specified parameters</returns>
        public bool CanHandle(IParameters parameters)
        {
            return parameters.MethodType == MethodType.Uuid;
        }
    }
}
