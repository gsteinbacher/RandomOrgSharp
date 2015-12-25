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
        /// Constructor used to pass information to the <see cref="CommonParameters"/> base class
        /// </summary>
        /// <param name="method">Method to call at random.org</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        private UuidParameters(MethodType method, bool verifyOriginator) : base(method, verifyOriginator) { }

        /// <summary>
        /// Create an instance of <see cref="UuidParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random UUID values you need. Must be between 1 and 1000.</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        /// <returns>Instance of <see cref="UuidParameters"/></returns>
        public static UuidParameters Create(int numberOfItemsToReturn, bool verifyOriginator = false)
        {
            var parameters = new UuidParameters(MethodType.Uuid, verifyOriginator);
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
        }
    }
}
