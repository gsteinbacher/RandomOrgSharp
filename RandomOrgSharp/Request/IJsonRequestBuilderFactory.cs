using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public interface IJsonRequestBuilderFactory {
        IJsonRequestBuilder GetBuilder(IParameters parameters);
    }
}