using System;
using System.Threading;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.Framework.Common.SystemWrapper.Interface;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp
{
    /// <summary>
    /// Manage the method calls to random.org.
    /// <remarks>
    /// This method maintains state for some information returned from the call to random.org.
    /// It is expected to only be instantiated once during the lifetime of the application.
    /// </remarks>
    /// </summary>
    public class AdvisoryDelayManager : IAdvisoryDelayManager
    {
        private readonly IDateTime _dateTimeWrap;
        private long _advisoryDelay;

        /// <summary>
        /// </summary>
        /// <summary>
        /// Instantiates an instance of <see cref="AdvisoryDelayManager" />  with <see cref="DateTimeWrap" /> as the class used to
        /// handle the instance of <see cref="DateTime" />.
        /// </summary>
        public AdvisoryDelayManager() : this(new DateTimeWrap()) { }

        /// <summary>
        /// Instantiates an instance of <see cref="AdvisoryDelayManager" />
        /// </summary>
        /// <param name="dateTimeWrap">Instance of <see cref="IDateTime" /> to handle <see cref="DateTime" /> processing</param>
        public AdvisoryDelayManager(IDateTime dateTimeWrap)
        {
            _dateTimeWrap = dateTimeWrap;
            _advisoryDelay = Settings.Default.LastResponse;
        }

        /// <summary>
        /// Wait until the advisory timeframe has elapsed before continuing
        /// </summary>
        public void Delay()
        {
            if (_advisoryDelay > 0)
            {
                var waitingTime = _dateTimeWrap.UtcNow.Ticks - _advisoryDelay;
                if (waitingTime > 0)
                    Thread.Sleep(TimeSpan.FromTicks(waitingTime * TimeSpan.TicksPerMillisecond));
            }
        }

        /// <summary>
        /// Store the advisory delay so it can be used in the <c>Delay</c> method
        /// </summary>
        /// <param name="advisoryDelay"></param>
        public void SetAdvisoryDelay(int advisoryDelay)
        {
            _advisoryDelay = _dateTimeWrap.UtcNow.Ticks + advisoryDelay;
        }
    }
}