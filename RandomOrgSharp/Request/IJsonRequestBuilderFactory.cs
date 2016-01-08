using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public interface IJsonRequestBuilderFactory {
        IJsonRequestBuilder GetBuilder(IParameters parameters);
    }
}