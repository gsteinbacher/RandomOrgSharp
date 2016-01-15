namespace Obacher.RandomOrgSharp.Core.Response
{
    public sealed class ErrorResponseInfo : IResponseInfo
    {
        public int Id { get; }
        public int AdvisoryDelay { get; }

        public int Code { get; private set; }
        public string Message { get; private set; }

        public ErrorResponseInfo(int id, int code, string message)
        {
            Id = id;
            Code = code;
            Message = message;

            AdvisoryDelay = 0;
        }
    }
}