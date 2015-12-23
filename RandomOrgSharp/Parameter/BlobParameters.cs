using Obacher.Framework.Common;

namespace Obacher.RandomOrgSharp.Parameter
{
    public enum BlobFormat
    {
        Base64,
        Hex
    }

    public class BlobParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 100;

        public int NumberOfItemsToReturn { get; private set; }
        public int Size { get; private set; }
        public BlobFormat Format { get; private set; }

        public void SetParameters(int numberOfItemsToReturn, int size, BlobFormat format)
        {
            if (!numberOfItemsToReturn.Between(1, MAX_ITEMS_ALLOWED))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(
                    StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MAX_ITEMS_ALLOWED));

            if (!size.Between(1, 1048576))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            if (size % 8 != 0)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.BLOB_SIZE_NOT_DIVISIBLE_BY_8));

            NumberOfItemsToReturn = numberOfItemsToReturn;
            Size = size;
            Format = format;

            Method = RandomOrgConstants.BLOB_METHOD;
        }
    }
}
