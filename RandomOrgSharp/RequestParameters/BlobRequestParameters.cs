using Newtonsoft.Json.Linq;
using Obacher.Framework.Common;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp.RequestParameters
{
    public enum BlobFormat
    {
        Base64,
        Hex
    }

    public class BlobRequestParameters : CommonRequestParameters
    {
        private const int MAX_ITEMS_ALLOWED = 100;

        private readonly int _numberOfItemsToReturn;
        private readonly int _size;
        private readonly BlobFormat _format;


        public BlobRequestParameters(int numberOfItemsToReturn, int size, BlobFormat format = BlobFormat.Base64)
        {
            if (!numberOfItemsToReturn.Between(1, MAX_ITEMS_ALLOWED))
                throw new RandomOrgRunTimeException(string.Format(Strings.ResourceManager.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE), MAX_ITEMS_ALLOWED));

            if (!size.Between(1, 1048576))
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            if (size % 8 != 0)
                throw new RandomOrgRunTimeException(Strings.ResourceManager.GetString(StringsConstants.BLOB_SIZE_NOT_DIVISIBLE_BY_8));

            _numberOfItemsToReturn = numberOfItemsToReturn;
            _size = size;
            _format = format;
        }

        public override JObject CreateJsonRequest()
        {

            JObject jsonParameters = new JObject(
                new JProperty(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME, _numberOfItemsToReturn),
                new JProperty(RandomOrgConstants.JSON_SIZE_PARAMETER_NAME, _size),
                new JProperty(RandomOrgConstants.JSON_FORMAT_PARAMETER_NAME, _format.ToString().ToLowerInvariant())
                );

            JObject jsonRequest = CreateJsonRequestInternal(RandomOrgConstants.BLOB_METHOD, jsonParameters);
            return jsonRequest;
        }
    }
}
