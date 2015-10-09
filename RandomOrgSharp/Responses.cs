using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp
{
    public interface IResponse
    {
    }

    public class RandomOrgIntegerResponse : RandomOrgResponse<int>
    {
        private RandomOrgIntegerResponse(string version, int[] data, DateTime completionTime, int bitsUsed, int bitsLeft,
       int requestsLeft, int advisoryDelay, int id) : base(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id)
        {
        }
        public static IResponse Parse(JObject json)
        {
            return ParseInternal(json);
        }
    }

    public class RandomOrgDecimalResponse : RandomOrgResponse<decimal>
    {
        private RandomOrgDecimalResponse(string version, decimal[] data, DateTime completionTime, int bitsUsed, int bitsLeft,
       int requestsLeft, int advisoryDelay, int id) : base(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id)
        {
        }

        public static IResponse Parse(JObject json)
        {
            return ParseInternal(json);
        }
    }


    public class RandomOrgStringResponse : RandomOrgResponse<string>
    {
        private RandomOrgStringResponse(string version, string[] data, DateTime completionTime, int bitsUsed, int bitsLeft,
       int requestsLeft, int advisoryDelay, int id) : base(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id)
        {
        }

        public static IResponse Parse(JObject json)
        {
            return ParseInternal(json);
        }
    }


    public class RandomOrgGuassianResponse : RandomOrgResponse<double>
    {
        private RandomOrgGuassianResponse(string version, double[] data, DateTime completionTime, int bitsUsed, int bitsLeft,
            int requestsLeft, int advisoryDelay, int id) : base(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id)
        {
        }

        public static IResponse Parse(JObject json)
        {
            return ParseInternal(json);
        }
    }

    public class RandomOrgUUIDResponse : RandomOrgResponse<Guid>
    {
        private RandomOrgUUIDResponse(string version, Guid[] data, DateTime completionTime, int bitsUsed, int bitsLeft,
            int requestsLeft, int advisoryDelay, int id) : base(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id)
        {
        }

        public static IResponse Parse(JObject json)
        {
            return ParseInternal(json);
        }
    }


    public class RandomOrgBlobResponse : RandomOrgResponse<string>
    {
        private RandomOrgBlobResponse(string version, string[] data, DateTime completionTime, int bitsUsed, int bitsLeft,
            int requestsLeft, int advisoryDelay, int id) : base(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id)
        {
        }

        public static IResponse Parse(JObject json)
        {
            return ParseInternal(json);
        }
    }
    
    public enum UsageStatus
    {
        Stopped,
        Paused,
        Running,
        Unknown
    }

    public class UsageResponse : IResponse
    {
        public string Version { get; private set; }
        public UsageStatus Status { get; private set; }
        public DateTime CreationTime { get; private set; }
        public int BitsLeft { get; private set; }
        public int RequestsLeft { get; private set; }
        public int TotalBits { get; private set; }
        public int TotalRequests { get; private set; }

        private UsageResponse(string version, UsageStatus status, DateTime creationTime, int bitsLeft, int requestsLeft, int totalBits, int totalRequests)
        {
            Version = version;
            Status = status;
            CreationTime = creationTime;
            BitsLeft = bitsLeft;
            RequestsLeft = requestsLeft;
            TotalBits = totalBits;
            TotalRequests = totalRequests;
        }

        public static UsageResponse Parse(JObject json)
        {
            var version = JsonHelper.JsonToString(json.GetValue(RandomOrgConstants.JSON_RPC_VALUE));
            var result = json.GetValue(RandomOrgConstants.JSON_RESULT_PARAMETER_NAME) as JObject;
            UsageStatus status = UsageStatus.Unknown;
            DateTime creationTime = DateTime.MinValue;
            int bitsLeft = 0;
            int requestsLeft = 0;
            int totalBits = 0;
            int totalRequests = 0;

            if (result != null)
            {
                var statusString = JsonHelper.JsonToString(json.GetValue(RandomOrgConstants.JSON_STATUS_PARAMETER_NAME));
                switch (statusString)
                {
                    case RandomOrgConstants.JSON_STATUS_STOPPED:
                        status = UsageStatus.Stopped;
                        break;
                    case RandomOrgConstants.JSON_STATUS_PAUSED:
                        status = UsageStatus.Paused;
                        break;
                    case RandomOrgConstants.JSON_STATUS_RUNNING:
                        status = UsageStatus.Running;
                        break;
                    default:
                        status = UsageStatus.Unknown;
                        break;
                }

                creationTime = JsonHelper.JsonToDateTime(json.GetValue(RandomOrgConstants.JSON_CREATION_TIME_PARAMETER_NAME));
                bitsLeft = JsonHelper.JsonToInt(json.GetValue(RandomOrgConstants.JSON_BITS_LEFT_PARAMETER_NAME));
                requestsLeft = JsonHelper.JsonToInt(json.GetValue(RandomOrgConstants.JSON_REQUESTS_LEFT_PARAMETER_NAME));
                totalBits = JsonHelper.JsonToInt(json.GetValue(RandomOrgConstants.JSON_TOTAL_BITS_PARAMETER_NAME));
                totalRequests = JsonHelper.JsonToInt(json.GetValue(RandomOrgConstants.JSON_TOTAL_REQUESTS_PARAMETER_NAME));
            }

            var usageResponse = new UsageResponse(version, status, creationTime, bitsLeft, requestsLeft, totalBits, totalRequests);
            return usageResponse;
        }
    }

    public class RandomOrgResponse<T> : IResponse
    {
        public string Version { get; private set; }
        public T[] Data { get; private set; }
        public DateTime CompletionTime { get; private set; }
        public int BitsUsed { get; private set; }
        public int BitsLeft { get; private set; }
        public int RequestsLeft { get; private set; }
        public int AdvisoryDelay { get; private set; }
        public int Id { get; private set; }

        protected RandomOrgResponse(string version, T[] data, DateTime completionTime, int bitsUsed, int bitsLeft, int requestsLeft, int advisoryDelay, int id)
        {
            Version = version;
            Data = data;
            CompletionTime = completionTime;
            BitsUsed = bitsUsed;
            BitsLeft = bitsLeft;
            RequestsLeft = requestsLeft;
            AdvisoryDelay = advisoryDelay;
            Id = id;
        }

        protected static RandomOrgResponse<T> ParseInternal(JObject json)
        {
            var version = JsonHelper.JsonToString(json.GetValue(RandomOrgConstants.JSON_RPC_PARAMETER_NAME));
            var result = json.GetValue(RandomOrgConstants.JSON_RESULT_PARAMETER_NAME) as JObject;
            var data = default(T[]);
            var completionTime = DateTime.MinValue;
            var bitsUsed = 0;
            var bitsLeft = 0;
            var requestsLeft = 0;
            var advisoryDelay = 0;

            if (result != null)
            {
                var random = result.GetValue(RandomOrgConstants.JSON_RANDOM_PARAMETER_NAME) as JObject;
                if (random != null)
                {
                    JArray dataArray = random.GetValue(RandomOrgConstants.JSON_DATA_PARAMETER_NAME) as JArray;
                    if (dataArray != null && dataArray.HasValues)
                        data = dataArray.Values<T>().ToArray();

                    completionTime = JsonHelper.JsonToDateTime(random.GetValue(RandomOrgConstants.JSON_COMPLETION_TIME_PARAMETER_NAME));
                }
                bitsUsed = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_BITS_USED_PARAMETER_NAME));
                bitsLeft = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_BITS_LEFT_PARAMETER_NAME));
                requestsLeft = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_REQUESTS_LEFT_PARAMETER_NAME));
                advisoryDelay = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_ADVISORY_DELAY_PARAMETER_NAME));
            }
            var id = JsonHelper.JsonToInt(json.GetValue("id"));

            var response = new RandomOrgResponse<T>(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id);
            return response;
        }
    }
}
