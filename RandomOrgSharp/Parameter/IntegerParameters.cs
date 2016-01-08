using Obacher.Framework.Common;

namespace Obacher.RandomOrgSharp.Core.Parameter
{
    /// <summary>
    /// Class which contains the parameters used when requesting random integer values from random.org
    /// </summary>
    public sealed class IntegerParameters : CommonParameters
    {
        private const int MaxItemsAllowed = 10000;

        public int NumberOfItemsToReturn { get; private set; }
        public int MinimumValue { get; private set; }
        public int MaximumValue { get; private set; }
        public bool AllowDuplicates { get; private set; }

        /// <summary>
        /// Constructor used to pass information to the <see cref="CommonParameters"/> base class
        /// </summary>
        /// <param name="method">Method to call at random.org</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        private IntegerParameters(MethodType method, bool verifyOriginator) : base(method, verifyOriginator) { }

        /// <summary>
        /// Create an instance of <see cref="IntegerParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random integer values you need. Must be between 1 and 10,000.</param>
        /// <param name="minimumValue">The lower boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000 and 1,000,000,000.</param>
        /// <param name="maximumValue">The upper boundary for the range from which the random numbers will be picked. Must be between -1,000,000,000a and 1,000,000,000.</param>
        /// <param name="allowDuplicates">True if duplicate values are allowed in the random values, default to <c>true</c></param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        /// <returns>Instance of <see cref="IntegerParameters"/></returns>
        public static IntegerParameters Create(int numberOfItemsToReturn, int minimumValue, int maximumValue, bool allowDuplicates = true, bool verifyOriginator = false)
        {
            var parameters = new IntegerParameters(MethodType.Integer, verifyOriginator);
            parameters.SetParameters(numberOfItemsToReturn, minimumValue, maximumValue, allowDuplicates);
            return parameters;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters(int numberOfItemsToReturn, int minimumValue, int maximumValue, bool allowDuplicates)
        {
            if (!numberOfItemsToReturn.Between(1, MaxItemsAllowed))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MaxItemsAllowed));

            if (!minimumValue.Between(-1000000000, 1000000000))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            if (!maximumValue.Between(-1000000000, 1000000000))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.MAXIMUM_VALUE_OUT_OF_RANGE));

            NumberOfItemsToReturn = numberOfItemsToReturn;
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            AllowDuplicates = allowDuplicates;
        }
    }
}
