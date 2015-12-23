using Obacher.Framework.Common;

namespace Obacher.RandomOrgSharp.Parameter
{
    public class DecimalParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 10000;

        public int NumberOfItemsToReturn { get; private set; }
        public int NumberOfDecimalPlaces { get; private set; }
        public bool AllowDuplicates { get; private set; }

        public void SetParameters(int numberOfItemsToReturn, int numberOfDecimalPlaces, bool allowDuplicates = true)
        {
            if (!numberOfItemsToReturn.Between(1, MAX_ITEMS_ALLOWED))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MAX_ITEMS_ALLOWED));

            if (!numberOfDecimalPlaces.Between(1, 20))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.MINIMUM_VALUE_OUT_OF_RANGE));

            NumberOfItemsToReturn = numberOfItemsToReturn;
            NumberOfDecimalPlaces = numberOfDecimalPlaces;
            AllowDuplicates = allowDuplicates;

            Method = RandomOrgConstants.DECIMAL_METHOD;
        }
    }


}
