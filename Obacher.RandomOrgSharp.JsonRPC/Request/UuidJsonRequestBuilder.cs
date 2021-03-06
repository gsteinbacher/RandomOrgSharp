﻿using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.JsonRPC.Request
{
    public class UuidJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Build(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var uuidParameters = parameters as UuidParameters;
            if (uuidParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "UuidParameters"));

            JObject jsonParameters = new JObject(
                new JProperty(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, uuidParameters.NumberOfItemsToReturn)
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
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return parameters.MethodType == MethodType.Uuid;
        }
    }
}
