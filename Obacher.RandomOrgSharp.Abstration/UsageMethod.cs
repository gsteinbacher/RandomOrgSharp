using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.Abstration
{
    /// <summary>
    /// Retrieve a lst of random blob values
    /// </summary>
    public class UsageMethod
    {
        private readonly IMethodCallBroker _usageMethodManager;

        /// <summary>
        /// Create an instance of <see cref="UsageMethod"/>.  
        /// </summary>
        /// <param name="usageMethodManager">UsageMethodManager class to use to retrieve the usage information.  Default is <see cref="UsageMethodManager"/></param>
        public UsageMethod(IMethodCallBroker usageMethodManager = null)
        {
            _usageMethodManager = usageMethodManager ?? new MethodCallBroker();
        }

        /// <summary>
        /// Returns information related to the the usage
        /// </summary>
        /// <returns>Information related to the usage</returns>
        public UsageResponseInfo GetUsage()
        {
            var parameters = UsageParameters.Create();

            var response = _usageMethodManager.Generate(parameters);
            return response as UsageResponseInfo;
        }

        /// <summary>
        /// Returns information related to the the usage as an asynchronous operation
        /// </summary>
        /// <returns>Information related to the usage</returns>
        public async Task<UsageResponseInfo> GetUsageAsync()
        {
            var parameters = UsageParameters.Create();

            var response = await _usageMethodManager.GenerateAsync(parameters);
            return response as UsageResponseInfo;
        }
    }
}
