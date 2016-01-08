using System.Net.NetworkInformation;
using Moq;
using Obacher.RandomOrgSharp;
using Obacher.RandomOrgSharp.Core;

namespace Obacher.UnitTest.Tools.Mocks
{
    public static class MockHelper
    {
        /// <summary>
        /// Mock the <see cref="RandomNumberGenerator"/> when generating an Id value that is stored in the request object
        /// </summary>
        /// <param name="id">Id to store, default is 0</param>
        /// <param name="randomNumberGeneratorMock">Random Number Generator mock object. a new instance is creeated if one is passed</param>
        /// <returns>The created mock object, or the same instance passed in if one is passed.</returns>
        public static Mock<IRandom> SetupIdMock(int id = 0, Mock<IRandom> randomNumberGeneratorMock = null)
        {
            if (randomNumberGeneratorMock == null)
                randomNumberGeneratorMock = new Mock<IRandom>();

            randomNumberGeneratorMock.Setup(m => m.Next())
                .Returns(id);

            return randomNumberGeneratorMock;
        }
    }
}