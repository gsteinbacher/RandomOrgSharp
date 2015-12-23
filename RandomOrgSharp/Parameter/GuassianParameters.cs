namespace Obacher.RandomOrgSharp.Parameter
{
    public class GuassianParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 10000;

        public int NumberOfItemsToReturn { get; private set; }
        public int Mean { get; private set; }
        public int StandardDeviation { get; private set; }
        public int SignificantDigits { get; private set; }

        public void SetParameters(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            if (numberOfItemsToReturn < 1 || numberOfItemsToReturn > MAX_ITEMS_ALLOWED)
                throw new RandomOrgRunTimeException(string.Format(ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE), MAX_ITEMS_ALLOWED));

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

            Method = RandomOrgConstants.GAUSSIAN_METHOD;
        }
    }
}
