namespace Obacher.RandomOrgSharp.Parameter
{
    /// <summary>
    /// Class which contains the parameters used when requesting random blob values from random.org
    /// </summary>
    public sealed class VerifySignatureParameters : CommonParameters
    {
        public string Result { get; private set; }
        public string Signature { get; private set; }

        /// <summary>
        /// Constructor used to pass information to the <see cref="CommonParameters"/> base class
        /// </summary>
        /// <param name="method">Method to call at random.org</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        private VerifySignatureParameters(MethodType method, bool verifyOriginator) : base(method, verifyOriginator) { }

        /// <summary>
        /// Create an instance of <see cref="VerifySignatureParameters"/>.
        /// </summary>
        /// <returns>Instance of <see cref="VerifySignatureParameters"/> with specified parameters set properly.</returns>
        public static VerifySignatureParameters Create(string result, string signature)
        {
            var parameters = new VerifySignatureParameters(MethodType.VerifySignature, false);
            parameters.SetParameters(result, signature);
            return parameters;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters(string result, string signature)
        {
            if (string.IsNullOrWhiteSpace(result))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.RESULT_REQUIRED));

            if (string.IsNullOrWhiteSpace(signature))
                throw new RandomOrgRunTimeException(ResourceHelper.GetString(StringsConstants.SIGNATURE_REQUIRED));

            Result = result;
            Signature = signature;
        }
    }
}
