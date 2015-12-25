using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp;

namespace Obacher.UnitTest.Tools.Mocks
{
    public class MockCommonParameters : IDisposable
    {
        public MockCommonParameters(int id = 0)
        {
            SettingsManager.Instance = ConfigMocks.SetupApiKeyMock().Object;
            RandomNumberGenerator.Instance = MockHelper.SetupIdMock(id).Object;
        }
        public void Dispose()
        {
            SettingsManager.Instance = null;
            RandomNumberGenerator.Instance = null;
        }
    }
}
