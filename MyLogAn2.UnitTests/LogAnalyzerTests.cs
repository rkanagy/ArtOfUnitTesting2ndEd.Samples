using System;
using NSubstitute;
using NUnit.Framework;

namespace MyLogAn2.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_TooShortFileName_CallLogger()
        {
            var logger = Substitute.For<ILogger>();
            var analyzer = new LogAnalyzer(logger)
            {
                MinNameLength = 6
            };

            analyzer.Analyze("a.txt");

            logger.Received().LogError("Filename too short: a.txt");
        }

        [Test]
        public void Returns_ByDefault_WorksForHardCodedArgument()
        {
            var fakeRules = Substitute.For<IFileNameRules>();

            fakeRules.IsValidLogFileName(Arg.Any<string>()).Returns(true);

            Assert.IsTrue(fakeRules.IsValidLogFileName("anything.txt"));
        }

        [Test]
        public void Returns_ArgAny_Throws()
        {
            var fakeRules = Substitute.For<IFileNameRules>();

            fakeRules
                .When(x => x.IsValidLogFileName(Arg.Any<string>()))
                .Do(context => throw new Exception("fake exception"));

            Assert.Throws<Exception>(() => fakeRules.IsValidLogFileName("anything.txt"));
        }

        [Test]
        public void Analyze_LoggerThrows_CallsWebService()
        {
            var mockWebService = new FakeWebService();
            var stubLogger = new FakeLogger2 { WillThrow = new Exception("fake exception") };

            var analyzer2 = new LogAnalyzer2(stubLogger, mockWebService) { MinNameLength = 8 };

            var tooShortFileName = "abc.txt";
            analyzer2.Analyze(tooShortFileName);

            Assert.That(mockWebService.MessageToWebService, Does.Contain("fake exception"));
        }
    }

    public class FakeWebService : IWebService
    {
        public string MessageToWebService;

        public void Write(string message)
        {
            MessageToWebService = message;
        }
    }

    public class FakeLogger2 : ILogger
    {
        public Exception WillThrow;

        public void LogError(string message)
        {
            if (WillThrow != null)
            {
                throw WillThrow;
            }
        }
    }
}
