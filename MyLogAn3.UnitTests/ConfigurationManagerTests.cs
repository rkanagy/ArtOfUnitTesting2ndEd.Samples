using NUnit.Framework;

namespace MyLogAn3.UnitTests
{
    [TestFixture]
    public class ConfigurationManagerTests
    {
        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            var configMgr = new ConfigurationManager();
            var configured = configMgr.IsConfigured("something");
            // rest of test
        }

        [TearDown]
        public void Teardown()
        {
            // need to reset a static resource between tests
            LoggingFacility.Logger = null;
        }
    }
}
