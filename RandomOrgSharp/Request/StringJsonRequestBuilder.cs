﻿using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public class StringJsonRequestBuilder : IJsonRequestBuilder
    {
        public JObject Create(IParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var stringParameters = parameters as StringParameters;
            if (stringParameters == null)
                throw new ArgumentException(ResourceHelper.GetString(StringsConstants.EXCEPTION_INVALID_ARGUMENT, "StringParameters"));

            var jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, stringParameters.NumberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_LENGTH_PARAMETER_NAME, stringParameters.Length),
                new JProperty(RandomOrgConstants.JSON_CHARACTERS_ALLOWED_PARAMETER_NAME, stringParameters.CharactersAllowed),
                new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, stringParameters.AllowDuplicates)
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
