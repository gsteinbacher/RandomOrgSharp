using System;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public class UsageResponseInfo : IResponseInfo
    {
        public string Version { get; }

        public StatusType Status { get; }
        public DateTime CreationTime { get; }
        public int BitsLeft { get; }
        public int RequestsLeft { get; }
        public int TotalBits { get; }
        public int TotalRequests { get; }
        public int Id { get; }
        public int AdvisoryDelay { get; }

        public UsageResponseInfo(string version, StatusType status, DateTime creationTime, int bitsLeft, int requestsLeft, int totalBits, int totalRequests, int id)
        {
            Version = version;
            Status = status;
            CreationTime = creationTime;
            BitsLeft = bitsLeft;
            RequestsLeft = requestsLeft;
            TotalBits = totalBits;
            TotalRequests = totalRequests;
            Id = id;

            // Usage method does not return Advisory Delay so always set it to zero
            AdvisoryDelay = 0;
        }
    }
}
