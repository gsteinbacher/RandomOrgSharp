using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.RequestParameters
{
    public class UUIDRequestParameters : CommonRequestParameters
    {
        private readonly int _numberOfItemsToReturn;

        public UUIDRequestParameters(int numberOfItemsToReturn)
        {
            if (numberOfItemsToReturn < 1 || numberOfItemsToReturn > 1000)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE));

            _numberOfItemsToReturn = numberOfItemsToReturn;
        }

        public override JObject CreateJsonRequest()
        {

            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _numberOfItemsToReturn)
                );

            JObject jsonRequest = CreateJsonRequestInternal(RandomOrgConstants.UUID_METHOD, jsonParameters);
            return jsonRequest;
        }
    }
}
