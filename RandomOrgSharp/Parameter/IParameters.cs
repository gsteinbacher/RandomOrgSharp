namespace Obacher.RandomOrgSharp.Core.Parameter
{
    public interface IParameters
    {
        int Id { get; }
        MethodType MethodType { get; }
        bool VerifyOriginator { get; set; }
    }
}