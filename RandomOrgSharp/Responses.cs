using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp
{
    public interface IResponse
    {
    }

    public abstract class RandomOrgResponse : IResponse
    {
        protected static string JsonToString(JToken token, string defaultValue = null)
        {
            if (token == null)
                return defaultValue;

            return token.ToString();
        }

        protected static int JsonToInt(JToken token, int defaultValue = 0)
        {
            if (token == null)
                return defaultValue;

            int returnValue;
            return !int.TryParse(token.ToString(), out returnValue) ? defaultValue : returnValue;
        }

        protected static DateTime JsonToDateTime(JToken token, DateTime? defaultValue = null)
        {
            DateTime localDefaultValue = defaultValue ?? DateTime.MinValue;

            if (token == null)
                return localDefaultValue;

            DateTime returnValue;
            return !DateTime.TryParse(token.ToString(), out returnValue) ? localDefaultValue : returnValue;
        }
    }


    public enum UsageStatus
    {
        Stopped,
        Paused,
        Running,
        Unknown
    }

    public class UsageResponse : RandomOrgResponse
    {
        public string Version { get; private set; }

        public UsageStatus Status { get; private set; }
        public DateTime CreationTime { get; private set; }
        public int BitsLeft { get; private set; }
        public int RequestsLeft { get; private set; }
        public int TotalBits { get; private set; }
        public int TotalRequest { get; set; }
        public int TotalRequests { get; private set; }

        private UsageResponse(string version, UsageStatus status, DateTime creationTime, int bitsLeft, int requestsLeft, int totalBits, int totalRequest)
        {
            Version = version;
            Status = status;
            CreationTime = creationTime;
            BitsLeft = bitsLeft;
            RequestsLeft = requestsLeft;
            TotalBits = totalBits;
            TotalRequest = totalRequest;
        }

        public static UsageResponse Parse(JObject json)
        {
            var version = JsonToString(json.GetValue("jsonrpc"));
            var result = json.GetValue("result") as JObject;
            UsageStatus status = UsageStatus.Unknown;
            DateTime creationTime = DateTime.MinValue;
            int bitsLeft = 0;
            int requestsLeft = 0;
            int totalBits = 0;
            int totalRequests = 0;

            if (result != null)
            {
                var statusString = JsonToString(json.GetValue("status"));
                switch (statusString)
                {
                    case "stopped":
                        status = UsageStatus.Stopped;
                        break;
                    case "paused":
                        status = UsageStatus.Paused;
                        break;
                    case "running":
                        status = UsageStatus.Running;
                        break;
                    default:
                        status = UsageStatus.Unknown;
                        break;
                }

                creationTime = JsonToDateTime(json.GetValue("creationTime"));
                bitsLeft = JsonToInt(json.GetValue("bitsLeft"));
                requestsLeft = JsonToInt(json.GetValue("requestsLeft"));
                totalBits = JsonToInt(json.GetValue("totalBits"));
                totalRequests = JsonToInt(json.GetValue("totalRequests"));
            }

            var usageResponse = new UsageResponse(version, status, creationTime, bitsLeft, requestsLeft, totalBits, totalRequests);
            return usageResponse;
        }
    }

    public class RandomOrgIntegerResponse : RandomOrgResponse
    {
        public static RandomOrgResponse Parse(JObject json)
        {
            return RandomOrgResponse<int>.Parse(json);
        }
    }

    public class RandomOrgDecimalResponse : RandomOrgResponse
    {
        public static RandomOrgResponse Parse(JObject json)
        {
            return RandomOrgResponse<decimal>.Parse(json);
        }
    }


    public class RandomOrgStringResponse : RandomOrgResponse
    {
        public static RandomOrgResponse Parse(JObject json)
        {
            return RandomOrgResponse<string>.Parse(json);
        }
    }


    public class RandomOrgUUIDResponse : RandomOrgResponse
    {
        public static RandomOrgResponse Parse(JObject json)
        {
            return RandomOrgResponse<Guid>.Parse(json);
        }
    }


    public class RandomOrgBlobResponse : RandomOrgResponse
    {
        public static RandomOrgResponse Parse(JObject json)
        {
            return RandomOrgResponse<string>.Parse(json);
        }
    }

    class RandomOrgResponse<T> : RandomOrgResponse
    {
        public string Version { get; private set; }
        public T[] Data { get; private set; }
        public DateTime CompletionTime { get; private set; }
        public int BitsUsed { get; private set; }
        public int BitsLeft { get; private set; }
        public int RequestsLeft { get; private set; }
        public int AdvisoryDelay { get; private set; }
        public int Id { get; private set; }

        private RandomOrgResponse(string version, T[] data, DateTime completionTime, int bitsUsed, int bitsLeft, int requestsLeft, int advisoryDelay, int id)
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

        public static RandomOrgResponse<T> Parse(JObject json)
        {
            var version = JsonToString(json.GetValue("jsonrpc"));
            var result = json.GetValue("result") as JObject;
            var data = default(T[]);
            var completionTime = DateTime.MinValue;
            var bitsUsed = 0;
            var bitsLeft = 0;
            var requestsLeft = 0;
            var advisoryDelay = 0;

            if (result != null)
            {
                var random = result.GetValue("random") as JObject;
                if (random != null)
                {
                    JArray dataArray = random.GetValue("data") as JArray;
                    if (dataArray != null && dataArray.HasValues)
                        data = dataArray.Values<T>().ToArray();

                    completionTime = JsonToDateTime(random.GetValue("completionTime"));
                }
                bitsUsed = JsonToInt(result.GetValue("bitsUsed"));
                bitsLeft = JsonToInt(result.GetValue("bitsLeft"));
                requestsLeft = JsonToInt(result.GetValue("requestsLeft"));
                advisoryDelay = JsonToInt(result.GetValue("advisoryDelay"));
            }
            var id = JsonToInt(json.GetValue("id"));

            var response = new RandomOrgResponse<T>(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id);
            return response;
        }
    }
}
