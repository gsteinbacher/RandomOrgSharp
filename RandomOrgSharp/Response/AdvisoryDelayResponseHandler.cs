using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class AdvisoryDelayResponseHandler : IResponseHandler
    {
        private readonly IAdvisoryDelayManager _advisoryDelayManager;

        public AdvisoryDelayResponseHandler(IAdvisoryDelayManager advisoryDelayManager)
        {
            _advisoryDelayManager = advisoryDelayManager;
        }

        /// <summary>
        /// Set the advisory delay time so it can be used in the <c>Delay</c> method
        /// </summary>
        /// <param name="json"></param>
        /// <param name="parameters"></param>
        public bool Process(JObject json, IParameters parameters)
        {
            var result = json.GetValue(RandomOrgConstants.JSON_RESULT_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                int advisoryDelay = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_ADVISORY_DELAY_PARAMETER_NAME));
                _advisoryDelayManager.SetAdvisoryDelay(advisoryDelay);
            }

            // If we get down to here then the Ids match
            return true;
        }

        /// <summary>
        /// Wait until the advisory timeframe has elapsed before continuing
        /// </summary>
        public void Delay()
        {
            _advisoryDelayManager.Delay();
        }
    }
}