namespace Obacher.RandomOrgSharp.Parameter
{
    public class UUIDParameters : CommonParameters
    {
        private const int MAX_ITEMS_ALLOWED = 1000;

        public int NumberOfItemsToReturn { get; private set; }

        public void SetParameters(int numberOfItemsToReturn)
        {
            if (numberOfItemsToReturn < 1 || numberOfItemsToReturn > MAX_ITEMS_ALLOWED)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.NUMBER_ITEMS_RETURNED_OUT_OF_RANGE, MAX_ITEMS_ALLOWED));

            NumberOfItemsToReturn = numberOfItemsToReturn;

            Method = RandomOrgConstants.UUID_METHOD;
        }
    }
}
