using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.RequestBuilder
{
    public class UUIDJsonParameterBuilder : IParameterBuilder
    {
        private readonly UUIDParameters _parameters;

        public UUIDJsonParameterBuilder(UUIDParameters parameters)
        {
            _parameters = parameters;
        }

        public JObject Create()
        {
            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _parameters.NumberOfItemsToReturn)
            );

            return jsonParameters;
        }
    }
}
