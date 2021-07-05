using Glasswall.CloudSdk.Common.Web.Models;
using NUnit.Framework;
using System;

namespace Glasswall.CloudSdk.AWS.Rebuild.Tests.RebuildControllerTests.RebuildFromBase64Method
{
    [TestFixture]
    public class WhenRebuildThrows : RebuildControllerTestBase
    {
        private const string Version = "Some Version";

        private Exception _dummyException;

        [OneTimeSetUp]
        public void OnetimeSetup()
        {
            CommonSetup();

            GlasswallVersionServiceMock.Setup(s => s.GetVersion())
                .Throws(_dummyException = new Exception());
        }

        [Test]
        public void Exception_Is_Rethrown()
        {
            Assert.That(() => ClassInTest.RebuildFromBase64(new Base64Request
            {
                Base64 = "dGVzdA=="
            }), Throws.Exception.EqualTo(_dummyException));
        }
    }
}