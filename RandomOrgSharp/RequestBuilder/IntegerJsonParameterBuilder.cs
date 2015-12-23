using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.RequestBuilder
{
    public class IntegerJsonParameterBuilder : IParameterBuilder
    {
        private readonly IntegerParameters _parameters;

        public IntegerJsonParameterBuilder(IntegerParameters parameters)
        {
            _parameters = parameters;
        }

        public JObject Create()
        {
            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _parameters.NumberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_MINIMUM_VALUE_PARAMETER_NAME, _parameters.MinimumValue),
                new JProperty(RandomOrgConstants.JSON_MAXIMUM_VALUE_PARAMETER_NAME, _parameters.MaximumValue),
                new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, _parameters.AllowDuplicates),
                new JProperty(RandomOrgConstants.JSON_BASE_NUMBER_PARAMETER_NAME, (int)_parameters.BaseNumber)
            );

            return jsonParameters;
        }
    }
}
