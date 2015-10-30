using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obacher.Framework.Common.SystemWrapper;

namespace Obacher.Framework.Common.UnitTest.SystemWrapper
{
    [TestClass]
    public class ConfigurationManagerWrapTest
    {
        private ConfigurationManagerWrap _mgr;

        [TestInitialize]
        public void Setup()
        {
            _mgr = new ConfigurationManagerWrap();
        }

        [TestMethod]
        public void AppSettings_Does_Not_Return_Null()
        {
            Assert.IsNotNull(_mgr.AppSettings);
        }

        [TestMethod]
        public void ConnectionStrings_Does_Not_Return_Null()
        {
            Assert.IsNotNull(_mgr.ConnectionStrings);
        }

        [TestMethod]
        public void GetSection_Does_Not_Throw_Exception()
        {
            _mgr.GetSection("section does not exist");
        }

        [TestMethod]
        public void OpenExeConfiguration_Does_Not_Throw_Exception()
        {
            _mgr.OpenExeConfiguration(It.IsAny<string>());
        }

        [TestMethod]
        public void OpenMachineConfiguration_Does_Not_Throw_Exception()
        {
            _mgr.OpenMachineConfiguration();
        }


        [TestMethod]
        public void RefreshSection_Does_Not_Throw_Exception()
        {
            _mgr.RefreshSection("section does not exist");
        }
    }
}