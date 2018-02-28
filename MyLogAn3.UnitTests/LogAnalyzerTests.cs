using NUnit.Framework;

namespace MyLogAn3.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests : BaseTestsClass
    {
        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            FakeTheLogger();

            var analyzer = new LogAnalyzer();
            analyzer.Analyze("myemptyfile.txt");
            // rest of test
        }
    }
}
