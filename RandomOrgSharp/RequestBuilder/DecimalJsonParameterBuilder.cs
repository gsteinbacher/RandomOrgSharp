using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.RequestBuilder
{
    public class DecimalParameterBuilder : IParameterBuilder
    {
        private readonly DecimalParameters _parameters;

        public DecimalParameterBuilder(DecimalParameters parameters)
        {
            _parameters = parameters;
        }

        public string GetMethod()
        {
            return _parameters.Method;
        }

        public JObject Create()
        {
            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _parameters.NumberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_DECIMAL_PLACES_PARAMETER_NAME, _parameters.NumberOfDecimalPlaces),
                new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, _parameters.AllowDuplicates)
            );

            return jsonParameters;
        }

    }
}
