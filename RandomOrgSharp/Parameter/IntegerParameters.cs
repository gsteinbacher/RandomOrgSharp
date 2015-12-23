using Obacher.Framework.Common;

namespace Obacher.RandomOrgSharp.Parameter
{
    /// <summary>
    /// Class which contains the parameters used when requesting random blob values from random.org
    /// </summary>
    public class IntegerParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 10000;

        public int NumberOfItemsToReturn { get; private set; }
        public int MinimumValue { get; private set; }
        public int MaximumValue { get; private set; }
        public bool AllowDuplicates { get; private set; }
        public BaseNumberOptions BaseNumber { get; private set; }

        /// <summary>
        /// Create an instance of <see cref="IntegerParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <param name="minimumValue">The lower boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000 and 1,000,000,000.</param>
        /// <param name="maximumValue">The upper boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000a and 1,000,000,000.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <param name="baseNumber">Specifies the base that will be used to display the numbers, default to Base 10</param>
        /// <returns>Instance of <see cref="IntegerParameters"/></returns>
        public static IntegerParameters Set(int numberOfItemsToReturn, int minimumValue, int maximumValue,
            bool allowDuplicates = true, BaseNumberOptions baseNumber = BaseNumberOptions.Ten)
        {
            var parameters = new IntegerParameters();
            parameters.SetParameters(numberOfItemsToReturn, minimumValue, maximumValue, allowDuplicates, baseNumber);
            return parameters;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters(int numberOfItemsToReturn, int minimumValue, int maximumValue, bool allowDuplicates, BaseNumberOptions baseNumber)
        {
            if (!numberOfItemsToReturn.Between(1, MAX_ITEMS_ALLOWED))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MAX_ITEMS_ALLOWED));

            if (!minimumValue.Between(-1000000000, 1000000000))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            if (!maximumValue.Between(-1000000000, 1000000000))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.MAXIMUM_VALUE_OUT_OF_RANGE));

            NumberOfItemsToReturn = numberOfItemsToReturn;
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            AllowDuplicates = allowDuplicates;
            BaseNumber = baseNumber;

            MethodType = MethodType.Integer;
        }
    }
}
