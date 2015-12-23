using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.RequestBuilder
{
    public class GuassianJsonParameterBuilder : IParameterBuilder
    {
        private readonly GuassianParameters _parameters;


        public GuassianJsonParameterBuilder(GuassianParameters parameters)
        {
            _parameters = parameters;
        }

        public JObject Create()
        {
            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _parameters.NumberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_MEAN_PARAMETER_NAME, _parameters.Mean),
                new JProperty(RandomOrgConstants.JSON_STANDARD_DEVIATION_PARAMETER_NAME, _parameters.StandardDeviation),
                new JProperty(RandomOrgConstants.JSON_SIGNIFICANT_DIGITS_PARAMETER_NAME, _parameters.SignificantDigits)
            );

            return jsonParameters;
        }
    }
}
