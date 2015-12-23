using System;

namespace Obacher.RandomOrgSharp.Response
{
    public interface IResponse
    {
        string Version { get; }
        DateTime CompletionTime { get; }
        int BitsUsed { get; }
        int BitsLeft { get; }
        int RequestsLeft { get; }
        int AdvisoryDelay { get; }
        int Id { get; }
    }
}