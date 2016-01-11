namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IErrorHandler
    {
        int Code { get; }
        string Message { get; }

        void Process(string response);
        bool HasError();
    }
}