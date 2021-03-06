﻿using System;
using System.Threading;
using Newtonsoft.Json.Linq;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.Framework.Common.SystemWrapper.Interface;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{
    public class AdvisoryDelayHandler : IRequestCommand, IResponseHandler
    {
        private readonly IDateTime _dateTimeWrap;
        private long _advisoryDelay;

        /// <summary>
        /// </summary>
        /// <summary>
        /// Instantiates an instance of <see cref="AdvisoryDelayHandler" />  with <see cref="DateTimeWrap" /> as the class used to
        /// handle the instance of <see cref="DateTime" />.
        /// </summary>
        public AdvisoryDelayHandler() : this(new DateTimeWrap()) { }

        /// <summary>
        /// Instantiates an instance of <see cref="AdvisoryDelayHandler" />
        /// </summary>
        /// <param name="dateTimeWrap">Instance of <see cref="IDateTime" /> to handle <see cref="DateTime" /> processing</param>
        public AdvisoryDelayHandler(IDateTime dateTimeWrap)
        {
            _dateTimeWrap = dateTimeWrap;
        }

        #region IRequestHandler implementation

        /// <summary>
        /// Wait the specified number of milliseconds before continuing so requests are not made to random.org too quickly
        /// </summary>
        /// <param name="parameters">Parameters passed into the request object</param>
        /// <returns>True</returns>
        public bool Process(IParameters parameters)
        {
            if (_advisoryDelay > 0)
            {
                var waitingTime = _dateTimeWrap.UtcNow.Ticks - _advisoryDelay;
                if (waitingTime > 0)
                    Thread.Sleep(TimeSpan.FromTicks(waitingTime * TimeSpan.TicksPerMillisecond));
            }
            return true;
        }

        public bool CanProcess(IParameters parameters)
        {
            return true;
        }

        #endregion

        #region IResponseHandler implementation

        public bool Handle(IParameters parameters, string response)
        {
            JObject jsonResponse = JObject.Parse(response);
            if (jsonResponse != null)
            {
                var result = jsonResponse.GetValue(JsonRpcConstants.RESULT_PARAMETER_NAME) as JObject;
                if (result != null)
                {
                    int advisoryDelay = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.ADVISORY_DELAY_PARAMETER_NAME, 0));

                    if (advisoryDelay == 0)
                        _advisoryDelay = 0;
                    else
                        _advisoryDelay = _dateTimeWrap.UtcNow.Ticks + advisoryDelay;
                }
            }
            return true;
        }

        /// <summary>
        /// Is the advisory delay needed for the current method call.  Currently advisory delay is used for all methods except GetUsage and VerifySignature
        /// </summary>
        /// <param name="parameters">Parameters passed into the request object</param>
        /// <returns>True if the method being called is not the GetUsage or VerifySignature method</returns>
        public bool CanHandle(IParameters parameters)
        {
            return parameters.MethodType != MethodType.Usage && parameters.MethodType != MethodType.VerifySignature;
        }

        #endregion
    }
}