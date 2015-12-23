using Obacher.Framework.Common;

namespace Obacher.RandomOrgSharp.Parameter
{
    public class IntegerParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 10000;

        public int NumberOfItemsToReturn { get; private set; }
        public int MinimumValue { get; private set; }
        public int MaximumValue { get; private set; }
        public bool AllowDuplicates { get; private set; }
        public BaseNumberOptions BaseNumber { get; private set; }

        public void SetParameters(int numberOfItemsToReturn, int minimumValue, int maximumValue,
            bool allowDuplicates = true, BaseNumberOptions baseNumber = BaseNumberOptions.Ten)
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

            Method = RandomOrgConstants.INTEGER_METHOD;
        }
    }
}
