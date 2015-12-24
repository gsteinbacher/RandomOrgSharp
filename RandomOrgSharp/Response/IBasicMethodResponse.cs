﻿using System;
using System.Collections.Generic;

namespace Obacher.RandomOrgSharp.Response
{
    public interface IBasicMethodResponse<out T> : IResponse
    {
        IEnumerable<T> Data { get; }
        DateTime CompletionTime { get; }
        int BitsUsed { get; }
        int AdvisoryDelay { get; }
    }
}