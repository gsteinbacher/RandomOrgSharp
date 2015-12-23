namespace Obacher.RandomOrgSharp.Parameter
{
    public class CommonParameters
    {
        public string ApiKey { get; private set; }

        public int Id { get; private set; }

        public string Method { get; protected set; }

        public CommonParameters() : this(new RandomNumberGenerator(), new SettingsManager())
        {
        }

        public CommonParameters(IRandom randomNumberGenerator, ISettingsManager settingsManager)
        {
            Id = randomNumberGenerator.Next();

            ApiKey = settingsManager.GetConfigurationValue<string>(RandomOrgConstants.APIKEY_KEY);
            if (ApiKey == null)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.APIKEY_REQUIRED));
        }
    }
}