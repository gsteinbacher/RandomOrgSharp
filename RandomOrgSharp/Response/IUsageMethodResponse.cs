using System;

namespace Obacher.RandomOrgSharp.Response
{
    public interface IUsageMethodResponse : IResponse
    {
        StatusType Status { get; }
        DateTime CreationTime { get; }
        int TotalBits { get; }
        int TotalRequests { get; }
    }
}