namespace Obacher.RandomOrgSharp.Core.Response
{
    public class VerifySignatureResponseInfo : IResponseInfo
    {
        public bool Authenticity { get; }
        public int Id { get; }

        public VerifySignatureResponseInfo(bool authenticity, int id)
        {
            Authenticity = authenticity;
            Id = id;
        }
    }
}

