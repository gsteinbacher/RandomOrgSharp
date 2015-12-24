using System;

namespace Obacher.RandomOrgSharp.Response
{
    public interface IResponse
    {
        string Version { get; }
        int BitsLeft { get; }
        int RequestsLeft { get; }
        int Id { get; }
    }
}