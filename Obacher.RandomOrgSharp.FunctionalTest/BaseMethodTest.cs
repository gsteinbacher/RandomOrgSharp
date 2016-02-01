using System;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Emulator;
using Obacher.RandomOrgSharp.JsonRPC.Response;

namespace RandomOrgSharp.FunctionalTest
{
    public class BaseMethodTest
    {
        public Random Random { get; private set; }
        public AdvisoryDelayHandler AdvisoryDelayHandler { get; private set; }
        public IRandomService Service { get; private set; }

        public BaseMethodTest()
        {
            Random = new Random();

            AdvisoryDelayHandler = new AdvisoryDelayHandler(new DateTimeWrap());

            if (new ConfigurationManagerWrap().GetAppSettingValue<bool>("useEmulator"))
                Service = new RandomOrgApiEmulator();
        }
    }
}