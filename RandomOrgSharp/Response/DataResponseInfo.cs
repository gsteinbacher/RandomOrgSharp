﻿using System;
using System.Collections.Generic;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public class DataResponseInfo<T> : IResponseInfo
    {
        public string Version { get; }
        public IEnumerable<T> Data { get; }
        public DateTime CompletionTime { get; }
        public int BitsUsed { get; }
        public int BitsLeft { get; }
        public int RequestsLeft { get; }
        public int AdvisoryDelay { get; }
        public int Id { get; }

        public DataResponseInfo(string version, IEnumerable<T> data, DateTime completionDate, int bitsUsed, int bitsLeft, int requestsLeft, int advisoryDelay, int id)
        {
            Version = version;
            Data = data;
            CompletionTime = completionDate;
            BitsUsed = bitsUsed;
            BitsLeft = bitsLeft;
            RequestsLeft = requestsLeft;
            AdvisoryDelay = advisoryDelay;
            Id = id;
        }
    }
}