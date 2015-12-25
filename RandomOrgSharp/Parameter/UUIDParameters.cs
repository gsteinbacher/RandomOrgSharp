namespace Obacher.RandomOrgSharp.Parameter
{
    /// <summary>
    /// Class which contains the parameters used when requesting random blob values from random.org
    /// </summary>
    public sealed class UuidParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 1000;

        public int NumberOfItemsToReturn { get; private set; }

        /// <summary>
        /// Create an instance of <see cref="UuidParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random UUID values you need. Must be between 1 and 1000.</param>
        /// <returns>Instance of <see cref="UuidParameters"/></returns>
        public static UuidParameters Set(int numberOfItemsToReturn)
        {
            var parameters = new UuidParameters();
            parameters.SetParameters(numberOfItemsToReturn);
            return parameters;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters(int numberOfItemsToReturn)
        {
            if (numberOfItemsToReturn < 1 || numberOfItemsToReturn > MAX_ITEMS_ALLOWED)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MAX_ITEMS_ALLOWED));

            NumberOfItemsToReturn = numberOfItemsToReturn;

            MethodType = MethodType.Uuid;
        }
    }
}
