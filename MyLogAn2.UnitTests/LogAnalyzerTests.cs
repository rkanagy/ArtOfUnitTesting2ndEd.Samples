using Castle.Core.Logging;
using NUnit.Framework;

namespace MyLogAn2.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_TooShortFileName_CallLogger()
        {
            var logger = new FakeLogger();
            var analyzer = new LogAnalyzer(logger)
            {
                MinNameLength = 6
            };

            analyzer.Analyze("a.txt");

            StringAssert.Contains("too short", logger.LastError);
        }
    }

    public class FakeLogger : ILogger
    {
        public string LastError;

        public void LogError(string message)
        {
            LastError = message;
        }
    }
}
