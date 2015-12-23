using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Request
{
    public interface IParameterBuilderFactory {
        IRequestBuilder GetBuilder(IParameters parameters);
    }
}