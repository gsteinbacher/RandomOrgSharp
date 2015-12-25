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
        /// Create an instance of <see cref="DecimalParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <param name="numberOfDecimalPlaces">The number of decimal places to use. Must be between 1 and 20</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <returns>Instance of <see cref="DecimalParameters"/></returns>
        public static DecimalParameters Set(int numberOfItemsToReturn, int numberOfDecimalPlaces, bool allowDuplicates = true)
        {
            var parameters = new DecimalParameters();
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

            MethodType = MethodType.Decimal;
        }
    }


}
