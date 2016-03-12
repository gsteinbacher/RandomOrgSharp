using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IResponseParser
    {
        IResponseInfo Parse(string response);
        bool CanParse(IParameters parameters);
    }
}
