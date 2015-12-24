using Obacher.Framework.Common;

namespace Obacher.RandomOrgSharp.Parameter
{
    public enum BlobFormat
    {
        Base64,
        Hex
    }

    /// <summary>
    /// Class which contains the parameters used when requesting random blob values from random.org
    /// </summary>
    public class BlobParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 100;

        public int NumberOfItemsToReturn { get; private set; }
        public int Size { get; private set; }
        public BlobFormat Format { get; private set; }

        private BlobParameters() { }

        /// <summary>
        /// Create an instance of <see cref="BlobParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random blob values you need. Must be between 1 and 100.</param>
        /// <param name="size">The size of each blob, measured in bits. Must be between 1 and 1048576 and must be divisible by 8.</param>
        /// <param name="format">Specifies the format in which the blobs will be returned, default value is Base64</param>
        /// <returns>Instance of <see cref="BlobParameters"/></returns>
        public static BlobParameters Set(int numberOfItemsToReturn, int size, BlobFormat format = BlobFormat.Base64)
        {
            var parameters = new BlobParameters();
            parameters.SetParameters(numberOfItemsToReturn, size, format);
            return parameters;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters(int numberOfItemsToReturn, int size, BlobFormat format)
        {
            if (!numberOfItemsToReturn.Between(1, MAX_ITEMS_ALLOWED))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MAX_ITEMS_ALLOWED));

            if (!size.Between(1, 1048576))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            if (size % 8 != 0)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.BLOB_SIZE_NOT_DIVISIBLE_BY_8));

            NumberOfItemsToReturn = numberOfItemsToReturn;
            Size = size;
            Format = format;

            MethodType = MethodType.Blob;
        }
    }
}
