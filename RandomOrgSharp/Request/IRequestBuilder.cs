using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core
{
    public interface IRequestBuilder
    {
        string Build(IParameters parameters);
    }
}