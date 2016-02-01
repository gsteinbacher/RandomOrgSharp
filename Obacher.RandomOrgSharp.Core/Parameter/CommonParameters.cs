namespace Obacher.RandomOrgSharp.Core.Parameter
{
    /// <summary>
    /// Class which contains the parameters that are used by all method calls to random.org
    /// </summary>
    public class CommonParameters : IParameters
    {
        public int Id { get; }
        public MethodType MethodType { get; }
        public bool VerifyOriginator { get; set; }

        public CommonParameters(MethodType method, bool verifyOriginator = false)
        {
            
            Id = RandomNumberGenerator.Instance.Next();

            MethodType = method;
            VerifyOriginator = verifyOriginator;
        }

        public string GetMethodName()
        {
            string methodName = null;

            switch (MethodType)
            {
                case MethodType.Integer:
                    methodName = VerifyOriginator ? RandomOrgConstants.INTEGER_SIGNED_METHOD : RandomOrgConstants.INTEGER_METHOD;
                    break;

                case MethodType.Decimal:
                    methodName = VerifyOriginator ? RandomOrgConstants.DECIMAL_SIGNED_METHOD : RandomOrgConstants.DECIMAL_METHOD;
                    break;

                case MethodType.Gaussian:
                    methodName = VerifyOriginator ? RandomOrgConstants.GAUSSIAN_SIGNED_METHOD : RandomOrgConstants.GAUSSIAN_METHOD;
                    break;

                case MethodType.String:
                    methodName = VerifyOriginator ? RandomOrgConstants.STRING_SIGNED_METHOD : RandomOrgConstants.STRING_METHOD;
                    break;

                case MethodType.Uuid:
                    methodName = VerifyOriginator ? RandomOrgConstants.UUID_SIGNED_METHOD : RandomOrgConstants.UUID_METHOD;
                    break;

                case MethodType.Blob:
                    methodName = VerifyOriginator ? RandomOrgConstants.BLOB_SIGNED_METHOD : RandomOrgConstants.BLOB_METHOD;
                    break;

                case MethodType.Usage:
                    methodName = RandomOrgConstants.USAGE_METHOD;
                    break;
            }

            return methodName;
        }
    }
}