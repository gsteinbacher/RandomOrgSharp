namespace Obacher.RandomOrgSharp.Parameter
{
    public interface IParameters
    {
        string ApiKey { get; }
        int Id { get; }
        MethodType MethodType { get; }
        bool VerifyOriginator { get; }
    }
}