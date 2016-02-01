namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IErrorHandler
    {
        ErrorResponseInfo ErrorInfo { get; }

        bool HasError();
    }
}