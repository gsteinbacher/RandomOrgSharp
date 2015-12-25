using System;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp.Parameter
{
    /// <summary>
    /// Class which contains the parameters used when requesting random blob values from random.org
    /// </summary>
    public class CommonParameters : IParameters
    {
        public string ApiKey { get; }

        public int Id { get; }

        public MethodType MethodType { get; protected set; }
        public bool VerifyOriginator { get; set; }

        public CommonParameters()
        {
            Id = RandomNumberGenerator.Instance.Next();

            ApiKey = SettingsManager.Instance.GetConfigurationValue<string>(RandomOrgConstants.APIKEY_KEY);
            if (ApiKey == null)
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.APIKEY_REQUIRED));
        }

        public string GetMethodName()
        {
            switch (MethodType)
            {
                case MethodType.Integer:
                    return VerifyOriginator ? RandomOrgConstants.INTEGER_SIGNED_METHOD : RandomOrgConstants.INTEGER_METHOD;

                case MethodType.Decimal:
                    return VerifyOriginator ? RandomOrgConstants.DECIMAL_SIGNED_METHOD : RandomOrgConstants.DECIMAL_METHOD;

                case MethodType.Gaussian:
                    return VerifyOriginator ? RandomOrgConstants.GAUSSIAN_SIGNED_METHOD : RandomOrgConstants.GAUSSIAN_METHOD;

                case MethodType.String:
                    return VerifyOriginator ? RandomOrgConstants.STRING_SIGNED_METHOD : RandomOrgConstants.STRING_METHOD;

                case MethodType.Uuid:
                    return VerifyOriginator ? RandomOrgConstants.UUID_SIGNED_METHOD : RandomOrgConstants.UUID_METHOD;

                case MethodType.Blob:
                    return VerifyOriginator ? RandomOrgConstants.BLOB_SIGNED_METHOD : RandomOrgConstants.BLOB_METHOD;

                case MethodType.Usage:
                    return RandomOrgConstants.USAGE_METHOD;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}