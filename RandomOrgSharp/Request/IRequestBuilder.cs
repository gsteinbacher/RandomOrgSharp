using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public interface IRequestBuilder
    {
        string Build(IParameters parameters);
    }
}