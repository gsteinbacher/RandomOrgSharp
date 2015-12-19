using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.RequestParameters
{
    public class GuassianRequestParameters : CommonRequestParameters
    {
        private readonly int _numberOfItemsToReturn;
        private readonly int _mean;
        private readonly int _standardDeviation;
        private readonly int _significantDigits;

        public GuassianRequestParameters(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            if (numberOfItemsToReturn < 1 || numberOfItemsToReturn > 10000)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE));

            if (mean < -1000000 || mean > 1000000)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.MEAN_VALUE_OUT_OF_RANGE));

            if (standardDeviation < -1000000 || standardDeviation > 1000000)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.STANDARD_DEVIATION_OUT_OF_RANGE));

            if (significantDigits < 2 || significantDigits > 20)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.SIGNIFICANT_DIGITS_OUT_OF_RANGE));

            _numberOfItemsToReturn = numberOfItemsToReturn;
            _mean = mean;
            _standardDeviation = standardDeviation;
            _significantDigits = significantDigits;
        }

        public override JObject CreateJsonRequest()
        {

            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _numberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_MEAN_PARAMETER_NAME, _mean),
                new JProperty(RandomOrgConstants.JSON_STANDARD_DEVIATION_PARAMETER_NAME, _standardDeviation),
                new JProperty(RandomOrgConstants.JSON_SIGNIFICANT_DIGITS_PARAMETER_NAME , _significantDigits)
                );

            JObject jsonRequest = CreateJsonRequestInternal(RandomOrgConstants.GAUSSIAN_METHOD, jsonParameters);
            return jsonRequest;
        }
    }
}
