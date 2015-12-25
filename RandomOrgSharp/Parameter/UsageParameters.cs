namespace Obacher.RandomOrgSharp.Parameter
{
    /// <summary>
    /// Class which contains the parameters used when requesting random blob values from random.org
    /// </summary>
    public sealed class UsageParameters : CommonParameters
    {
        /// <summary>
        /// Create an instance of <see cref="UsageParameters"/>.  There are no specific parameters needed for the Usage method
        /// so only the MethodType is set.
        /// </summary>
        /// <returns>Instance of <see cref="UsageParameters"/> with specified parameters set properly.</returns>
        public static UsageParameters Set()
        {
            var parameters = new UsageParameters();
            parameters.SetParameters();
            return parameters;
        }

        /// <summary>
        /// Validate and set the parameters properties
        /// </summary>
        private void SetParameters()
        {
            MethodType = MethodType.Usage;
        }
    }
}
