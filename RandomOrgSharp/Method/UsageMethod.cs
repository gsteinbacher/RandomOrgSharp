using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.Method
{
    /// <summary>
    /// Retrieve a lst of random blob values
    /// </summary>
    public class UsageMethod
    {
        private readonly IUsageMethodManager _usageMethodManager;

        /// <summary>
        /// Create an instance of <see cref="UsageMethod"/>.  
        /// </summary>
        /// <param name="usageMethodManager">UsageMethodManager class to use to retrieve the usage information.  Default is <see cref="UsageMethodManager"/></param>
        public UsageMethod(IUsageMethodManager usageMethodManager = null)
        {
            _usageMethodManager = usageMethodManager ?? new UsageMethodManager();
        }

        /// <summary>
        /// Returns information related to the the usage
        /// </summary>
        /// <returns>Information related to the usage</returns>
        public UsageResponse GetUsage()
        {
            var parameters = UsageParameters.Create();

            var response = _usageMethodManager.Get(parameters);
            return response;
        }

        /// <summary>
        /// Returns information related to the the usage as an asynchronous operation
        /// </summary>
        /// <returns>Information related to the usage</returns>
        public async Task<UsageResponse> GetUsageAsync()
        {
            var parameters = UsageParameters.Create();

            var response = await _usageMethodManager.GetAsync(parameters);
            return response;
        }
    }
}
