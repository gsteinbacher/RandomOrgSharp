using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public interface IJsonResponseParserFactory {
        IParser GetParser(IParameters parameters);
    }
}