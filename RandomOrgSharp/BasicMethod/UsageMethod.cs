using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Parameter;
using Obacher.RandomOrgSharp.Request;
using Obacher.RandomOrgSharp.Response;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    /// <summary>
    /// Retrieve a lst of random blob values
    /// </summary>
    public class UsageMethod
    {
        private readonly IBasicMethodManager<string> _basicMethodManager;

        /// <summary>
        /// Create an instance of <see cref="UsageMethod"/>.  
        /// </summary>
        /// <param name="basicMethodManager">basicMethodManager class to use to retrieve the usage information.  Default is <see cref="basicMethodManagerManager{T}"/></param>
        public UsageMethod(IBasicMethodManager<string> basicMethodManager = null)
        {
            _basicMethodManager = basicMethodManager ?? new BasicMethodManager<string>();
        }

        /// <summary>
        /// Returns information related to the the usage
        /// </summary>
        /// <returns>Information related to the usage</returns>
        public IBasicMethodResponse<string> GetUsage()
        {
            var parameters = UsageParameters.Create();

            var response = _basicMethodManager.Generate(parameters);
            return response;
        }

        /// <summary>
        /// Returns information related to the the usage as an asynchronous operation
        /// </summary>
        /// <returns>Information related to the usage</returns>
        public async Task<IBasicMethodResponse<string>> GetUsageAsync()
        {
            var parameters = UsageParameters.Create();

            var response = await _basicMethodManager.GenerateAsync(parameters);
            return response;
        }
    }
}
