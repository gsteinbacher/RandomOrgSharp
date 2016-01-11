namespace Obacher.RandomOrgSharp.Core.Response
{
    public class VerifySignatureResponseInfo : IResponseInfo
    {
        public bool Authenticity { get; }
        public int Id { get; }
        public int AdvisoryDelay { get; }

        public VerifySignatureResponseInfo(bool authenticity, int id)
        {
            Authenticity = authenticity;
            Id = id;

            // VerifySignature method does not return Advisory Delay so always set it to zero
            AdvisoryDelay = 0;
        }
    }
}

