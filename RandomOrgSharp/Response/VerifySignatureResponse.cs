namespace Obacher.RandomOrgSharp.Response
{
    public class VerifySignatureResponse : IResponse
    {
        public bool Authenticity { get; }
        public int Id { get; }

        public VerifySignatureResponse(bool authenticity, int id)
        {
            Authenticity = authenticity;
            Id = id;
        }
    }
}

