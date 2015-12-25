using Obacher.Framework.Common;

namespace Obacher.RandomOrgSharp.Parameter
{
    /// <summary>
    /// Class which contains the parameters used when requesting random decimal values from random.org
    /// </summary>
    public sealed class DecimalParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 10000;

        public int NumberOfItemsToReturn { get; private set; }
        public int NumberOfDecimalPlaces { get; private set; }
        public bool AllowDuplicates { get; private set; }

        /// <summary>
        /// Constructor used to pass information to the <see cref="CommonParameters"/> base class
        /// </summary>
        /// <param name="method">Method to call at random.org</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        private DecimalParameters(MethodType method, bool verifyOriginator) : base(method, verifyOriginator) { }

        /// <summary>
        /// Create an instance of <see cref="DecimalParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <param name="numberOfDecimalPlaces">The number of decimal places to use. Must be between 1 and 20</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        /// <returns>Instance of <see cref="DecimalParameters"/></returns>
        public static DecimalParameters Create(int numberOfItemsToReturn, int numberOfDecimalPlaces, bool allowDuplicates = true, bool verifyOriginator = false)
        {
            var parameters = new DecimalParameters(MethodType.Decimal, verifyOriginator);
            parameters.SetParameters(numberOfItemsToReturn, numberOfDecimalPlaces, allowDuplicates);
            return parameters;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters(int numberOfItemsToReturn, int numberOfDecimalPlaces, bool allowDuplicates)
        {
            if (!numberOfItemsToReturn.Between(1, MAX_ITEMS_ALLOWED))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MAX_ITEMS_ALLOWED));

            if (!numberOfDecimalPlaces.Between(1, 20))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            NumberOfItemsToReturn = numberOfItemsToReturn;
            NumberOfDecimalPlaces = numberOfDecimalPlaces;
            AllowDuplicates = allowDuplicates;
        }
    }


}
