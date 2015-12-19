using Newtonsoft.Json.Linq;
using Obacher.Framework.Common;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.RequestParameters
{
    public class IntegerRequestParameters : CommonRequestParameters
    {
        private const int MAX_ITEMS_ALLOWED = 10000;

        private readonly int _numberOfItemsToReturn;
        private readonly int _minimumValue;
        private readonly int _maximumValue;
        private readonly bool _allowDuplicates;
        private readonly BaseNumberOptions _baseNumber;

        public IntegerRequestParameters(int numberOfItemsToReturn, int minimumValue, int maximumValue,
            bool allowDuplicates = true, BaseNumberOptions baseNumber = BaseNumberOptions.Ten)
        {
            if (!numberOfItemsToReturn.Between(1, MAX_ITEMS_ALLOWED))
                throw new RandomOrgRunTimeException(string.Format(Strings.ResourceManager.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE), MAX_ITEMS_ALLOWED));

            if (!minimumValue.Between(-1000000000, 1000000000))
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            if (!maximumValue.Between(-1000000000, 1000000000))
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.MAXIMUM_VALUE_OUT_OF_RANGE));

            _numberOfItemsToReturn = numberOfItemsToReturn;
            _minimumValue = minimumValue;
            _maximumValue = maximumValue;
            _allowDuplicates = allowDuplicates;
            _baseNumber = baseNumber;
        }

        public override JObject CreateJsonRequest()
        {

            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _numberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_MINIMUM_VALUE_PARAMETER_NAME, _minimumValue),
                new JProperty(RandomOrgConstants.JSON_MAXIMUM_VALUE_PARAMETER_NAME, _maximumValue),
                new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, _allowDuplicates),
                new JProperty(RandomOrgConstants.JSON_BASE_NUMBER_PARAMETER_NAME, (int)_baseNumber)
                );

            JObject jsonRequest = CreateJsonRequestInternal(RandomOrgConstants.INTEGER_METHOD, jsonParameters);
            return jsonRequest;
        }
    }
}
