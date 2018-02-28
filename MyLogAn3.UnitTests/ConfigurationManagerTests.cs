using NUnit.Framework;

namespace MyLogAn3.UnitTests
{
    [TestFixture]
    public class ConfigurationManagerTests : BaseTestsClass
    {
        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            FakeTheLogger();
            var configMgr = new ConfigurationManager();
            var configured = configMgr.IsConfigured("something");
            // rest of test
        }
    }
}
