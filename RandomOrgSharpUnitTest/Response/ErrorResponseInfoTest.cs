using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Core.Response;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest.Response
{
    [TestClass]
    public class ErrorResponseInfoTest
    {
        [TestMethod]
        public void Constructor_WhenCalled_ExpectAllValuesSet()
        {
            const int code = 999;
            const string message = "Message";
            const int id = 400;

            ErrorResponseInfo target = new ErrorResponseInfo(id, code, message);

            target.Id.Should().Equal(id);
            target.Code.Should().Equal(code);
            target.Message.Should().Equal(message);
            target.AdvisoryDelay.Should().Equal(0);
        }
    }
}
