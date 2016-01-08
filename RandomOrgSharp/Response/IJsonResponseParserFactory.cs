using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IJsonResponseParserFactory {
        IParser GetParser(IParameters parameters);
    }
}