using System.CodeDom;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.RequestParameters
{
    public class IntegerRequestParameters : CommonRequestParameters
    {
        private readonly int _numberOfItemsToReturn;
        private readonly int _minimumValue;
        private readonly int _maximumValue;
        private readonly bool _allowDuplicates;
        private readonly BaseNumberOptions _baseNumber;

        public IntegerRequestParameters(int numberOfItemsToReturn, int minimumValue, int maximumValue,
            bool allowDuplicates = true, BaseNumberOptions baseNumber = BaseNumberOptions.Ten)
        {
            if (numberOfItemsToReturn < 1 || numberOfItemsToReturn > 9999)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(ResourceConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE));

            if (minimumValue < -1000000000 || minimumValue > 1000000000)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(ResourceConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            if (maximumValue < -1000000000 || maximumValue > 1000000000)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(ResourceConstants.MAXIMUM_VALUE_OUT_OF_RANGE));

            _numberOfItemsToReturn = numberOfItemsToReturn;
            _minimumValue = minimumValue;
            _maximumValue = maximumValue;
            _allowDuplicates = allowDuplicates;
            _baseNumber = baseNumber;
        }

        public override JObject CreateJsonRequest()
        {

            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _numberOfItemsToReturn),
                new JProperty(RandomOrgConstants.MINIMUM_VALUE_PARAMETER_NAME, _minimumValue),
                new JProperty(RandomOrgConstants.MAXIMUM_VALUE_PARAMETER_NAME, _maximumValue),
                new JProperty(RandomOrgConstants.REPLACEMENT_PARAMETER_NAME, _allowDuplicates),
                new JProperty(RandomOrgConstants.BASE_NUMBER_PARAMETER_NAME, (int)_baseNumber)
                );

            JObject jsonRequest = CreateJsonRequestInternal(RandomOrgConstants.INTEGER_METHOD, jsonParameters);
            return jsonRequest;
        }
    }
}
