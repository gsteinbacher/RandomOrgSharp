namespace Obacher.RandomOrgSharp.Core.Parameter
{
    /// <summary>
    /// Class which contains the parameters used when requesting random guassian values from random.org
    /// </summary>
    public sealed class GuassianParameters : CommonParameters
    {
        private const int MaxItemsAllowed = 10000;

        public int NumberOfItemsToReturn { get; private set; }
        public int Mean { get; private set; }
        public int StandardDeviation { get; private set; }
        public int SignificantDigits { get; private set; }

        /// <summary>
        /// Constructor used to pass information to the <see cref="CommonParameters"/> base class
        /// </summary>
        /// <param name="method">Method to call at random.org</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        private GuassianParameters(MethodType method, bool verifyOriginator) : base(method, verifyOriginator) { }

        /// <summary>
        /// Create an instance of <see cref="GuassianParameters"/>
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random guassian values you need. Must be between 1 and 10,000.</param>
        /// <param name="mean">The distribution's mean. Must be between -1,000,000 and 1,000,000.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must be between -1,000,000 and 1,000,000</param>
        /// <param name="significantDigits">The number of significant digits to use. Must be between 2 and 20.</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        /// <returns>Instance of <see cref="GuassianParameters"/></returns>
        public static GuassianParameters Create(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits, bool verifyOriginator = false)
        {
            var parameters = new GuassianParameters(MethodType.Gaussian, verifyOriginator);
            parameters.SetParameters(numberOfItemsToReturn, mean, standardDeviation, significantDigits);
            return parameters;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            if (numberOfItemsToReturn < 1 || numberOfItemsToReturn > MaxItemsAllowed)
                throw new RandomOrgRunTimeException(string.Format(ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE), MaxItemsAllowed));

            if (mean < -1000000 || mean > 1000000)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.MEAN_VALUE_OUT_OF_RANGE));

            if (standardDeviation < -1000000 || standardDeviation > 1000000)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.STANDARD_DEVIATION_OUT_OF_RANGE));

            if (significantDigits < 2 || significantDigits > 20)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.SIGNIFICANT_DIGITS_OUT_OF_RANGE));

            NumberOfItemsToReturn = numberOfItemsToReturn;
            Mean = mean;
            StandardDeviation = standardDeviation;
            SignificantDigits = significantDigits;
        }
    }
}
