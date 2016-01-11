using System;
using System.Threading;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.Framework.Common.SystemWrapper.Interface;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Properties;
using Obacher.RandomOrgSharp.Core.Request;

namespace Obacher.RandomOrgSharp.Core.Response
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
            _advisoryDelay = Settings.Default.LastResponse;
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

        #endregion

        #region IResponseHandler implementation

        /// <summary>
        /// Set the advisory delay time so it can be used to delay a request that is occurring before the advised delay time
        /// </summary>
        /// <param name="parameters">Parameters passed into the request object</param>
        /// <param name="info">ResponseInfo object</param>
        public bool Process(IParameters parameters, IResponseInfo info)
        {
            if (info.AdvisoryDelay == 0)
                _advisoryDelay = 0;
            else
                _advisoryDelay = _dateTimeWrap.UtcNow.Ticks + info.AdvisoryDelay;


            // If we get down to here then the Ids match
            return true;
        }

        /// <summary>
        /// Is the advisory delay needed for the current method call.  Currently advisory delay is used for all methods except GetUsage
        /// </summary>
        /// <param name="parameters">Parameters passed into the request object</param>
        /// <returns>True if the method being called is not the GetUsage method</returns>
        public bool CanHandle(IParameters parameters)
        {
            return parameters.MethodType != MethodType.Usage;
        }

        #endregion
    }
}