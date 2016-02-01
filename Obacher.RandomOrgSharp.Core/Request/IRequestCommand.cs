using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public interface IRequestCommand
    {
        bool Process(IParameters parameters);
        bool CanProcess(IParameters parameters);
    }
}