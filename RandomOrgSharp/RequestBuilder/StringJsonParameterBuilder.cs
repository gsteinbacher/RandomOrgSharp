using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.RequestBuilder
{

    public class StringRequestParameters : IParameterBuilder
    {
        private readonly StringParameters _parameters;

        public StringRequestParameters(StringParameters parameters)
        {
            _parameters = parameters;
        }

        public JObject Create()
        {
            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _parameters.NumberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_LENGTH_PARAMETER_NAME, _parameters.Length),
                new JProperty(RandomOrgConstants.JSON_CHARACTERS_ALLOWED_PARAMETER_NAME, _parameters.Allowed),
                new JProperty(RandomOrgConstants.JSON_REPLACEMENT_PARAMETER_NAME, _parameters.AllowDuplicates)
            );

            return jsonParameters;
        }
    }
}
