using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.RequestParameters
{
    public class UUIDRequestParameters : CommonRequestParameters
    {
        private const int MAX_ITEMS_ALLOWED = 1000;

        private readonly int _numberOfItemsToReturn;

        public UUIDRequestParameters(int numberOfItemsToReturn)
        {
            if (numberOfItemsToReturn < 1 || numberOfItemsToReturn > MAX_ITEMS_ALLOWED)
                throw new RandomOrgRunTimeException(string.Format(Strings.ResourceManager.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE), MAX_ITEMS_ALLOWED));

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
