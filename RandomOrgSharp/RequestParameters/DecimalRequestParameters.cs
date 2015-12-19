using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.RequestParameters
{
    public class DecimalRequestParameters : CommonRequestParameters
    {
        private readonly int _numberOfItemsToReturn;
        private readonly int _numberOfDecimalPlaces;
        private readonly bool _allowDuplicates;
        private readonly BaseNumberOptions _baseNumber;

        public DecimalRequestParameters(int numberOfItemsToReturn, int numberOfDecimalPlaces, 
            bool allowDuplicates = true, BaseNumberOptions baseNumber = BaseNumberOptions.Ten)
        {
            if (numberOfItemsToReturn < 1 || numberOfItemsToReturn > 10000)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE));

            if (numberOfDecimalPlaces < 1 || numberOfDecimalPlaces > 20)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            _numberOfItemsToReturn = numberOfItemsToReturn;
            _numberOfDecimalPlaces = numberOfDecimalPlaces;
            _allowDuplicates = allowDuplicates;
            _baseNumber = baseNumber;
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
