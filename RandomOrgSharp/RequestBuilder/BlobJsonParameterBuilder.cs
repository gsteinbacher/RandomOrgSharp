using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.RequestBuilder
{
    public class BlobJsonParameterBuilder : IParameterBuilder
    {
        private readonly BlobParameters _parameters;

        public BlobJsonParameterBuilder(BlobParameters parameters)
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
                new JProperty(RandomOrgConstants.JSON_SIZE_PARAMETER_NAME, _parameters.Size),
                new JProperty(RandomOrgConstants.JSON_FORMAT_PARAMETER_NAME, _parameters.Format.ToString().ToLowerInvariant())
            );

            return jsonParameters;
        }
    }
}
