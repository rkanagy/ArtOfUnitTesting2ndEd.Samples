using NUnit.Framework;

namespace MyLogAn3.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            var analyzer = new LogAnalyzer();
            analyzer.Analyze("myemptyfile.txt");
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
