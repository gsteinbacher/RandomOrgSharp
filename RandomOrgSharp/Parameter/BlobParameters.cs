using Obacher.Framework.Common;

namespace Obacher.RandomOrgSharp.Core.Parameter
{
    public enum BlobFormat
    {
        Base64,
        Hex
    }

    /// <summary>
    /// Class which contains the parameters used when requesting random blob values from random.org
    /// </summary>
    public sealed class BlobParameters : CommonParameters
    {
        private const int MaxItemsAllowed = 100;

        public int NumberOfItemsToReturn { get; private set; }
        public int Size { get; private set; }
        public BlobFormat Format { get; private set; }

        /// <summary>
        /// Constructor used to pass information to the <see cref="CommonParameters"/> base class
        /// </summary>
        /// <param name="method">Method to call at random.org</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        private BlobParameters(MethodType method, bool verifyOriginator) : base(method, verifyOriginator) { }

        /// <summary>
        /// Create an instance of <see cref="BlobParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random blob values you need. Must be between 1 and 100.</param>
        /// <param name="size">The size of each blob, measured in bits. Must be between 1 and 1048576 and must be divisible by 8.</param>
        /// <param name="format">Specifies the format in which the blobs will be returned, default value is Base64</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        /// <returns>Instance of <see cref="BlobParameters"/></returns>
        public static BlobParameters Create(int numberOfItemsToReturn, int size, BlobFormat format = BlobFormat.Base64, bool verifyOriginator = false)
        {
            var parameters = new BlobParameters(MethodType.Blob, verifyOriginator);
            parameters.SetParameters(numberOfItemsToReturn, size, format);
            return parameters;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters(int numberOfItemsToReturn, int size, BlobFormat format)
        {
            if (!numberOfItemsToReturn.Between(1, MaxItemsAllowed))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MaxItemsAllowed));

            if (!size.Between(1, 1048576))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            if (size % 8 != 0)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.BLOB_SIZE_NOT_DIVISIBLE_BY_8));

            NumberOfItemsToReturn = numberOfItemsToReturn;
            Size = size;
            Format = format;
        }
    }
}
