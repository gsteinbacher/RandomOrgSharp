namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IResponseParser
    {
        IResponseInfo Parse(string response);
    }
}
