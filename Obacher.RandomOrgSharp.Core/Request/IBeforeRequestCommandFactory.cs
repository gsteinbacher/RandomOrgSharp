using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    /// <summary>
    /// Code that will be executed prior to the <see cref="IRandomService"/> being called
    /// </summary>
    public interface IBeforeRequestCommandFactory
    {
        /// <summary>
        /// Handle the command
        /// </summary>
        /// <param name="parameters">Parameters that are being passed into the request</param>
        /// <returns></returns>
        bool Execute(IParameters parameters);
    }
}
