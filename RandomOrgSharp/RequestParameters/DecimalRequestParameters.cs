﻿using Newtonsoft.Json.Linq;
using Obacher.Framework.Common;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.RequestParameters
{
    public class DecimalRequestParameters : CommonRequestParameters
    {
        private const int MAX_ITEMS_ALLOWED = 10000;

        private readonly int _numberOfItemsToReturn;
        private readonly int _numberOfDecimalPlaces;
        private readonly bool _allowDuplicates;

        public DecimalRequestParameters(int numberOfItemsToReturn, int numberOfDecimalPlaces, bool allowDuplicates = true)
        {
            if (!numberOfItemsToReturn.Between(1, MAX_ITEMS_ALLOWED))
                throw new RandomOrgRunTimeException(string.Format(Strings.ResourceManager.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE), MAX_ITEMS_ALLOWED));

            if (!numberOfDecimalPlaces.Between(1, 20))
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            _numberOfItemsToReturn = numberOfItemsToReturn;
            _numberOfDecimalPlaces = numberOfDecimalPlaces;
            _allowDuplicates = allowDuplicates;
        }

        public override JObject CreateJsonRequest()
        {

            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _numberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_DECIMAL_PLACES_PARAMETER_NAME, _numberOfDecimalPlaces),
                new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, _allowDuplicates)
                );

            JObject jsonRequest = CreateJsonRequestInternal(RandomOrgConstants.DECIMAL_METHOD, jsonParameters);
            return jsonRequest;
        }
    }
}
