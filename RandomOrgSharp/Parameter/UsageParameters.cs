namespace Obacher.RandomOrgSharp.Core.Parameter
{
    /// <summary>
    /// Class which contains the parameters used when requesting random blob values from random.org
    /// </summary>
    public sealed class UsageParameters : CommonParameters
    {
        /// <summary>
        /// Constructor used to pass information to the <see cref="CommonParameters"/> base class
        /// </summary>
        /// <param name="method">Method to call at random.org</param>
        /// <param name="verifyOriginator">Verify that the response is what was sent by random.org and it was not tampered with before receieved</param>
        private UsageParameters(MethodType method, bool verifyOriginator) : base(method, verifyOriginator) { }

        /// <summary>
        /// Build an instance of <see cref="UsageParameters"/>.  There are no specific parameters needed for the Usage method
        /// so only the MethodType is set.
        /// </summary>
        /// <returns>Instance of <see cref="UsageParameters"/> with specified parameters set properly.</returns>
        public static UsageParameters Create()
        {
            var parameters = new UsageParameters(MethodType.Usage, false);
            parameters.SetParameters();
            return parameters;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters() { }
    }
}
