using System;
using System.Configuration.Internal;
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
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();
            stubLogger
                .When(logger => logger.LogError(Arg.Any<string>()))
                .Do(info => throw new Exception("fake exception"));
            var analyzer = new LogAnalyzer2(stubLogger, mockWebService) { MinNameLength = 10 };

            analyzer.Analyze("Short.txt");

            mockWebService.Received()
                .Write(Arg.Is<string>(s => s.Contains("fake exception")));
        }

        [Test]
        public void Analyze_LoggerThrows_CallsWebServiceWithNSubObject()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();
            stubLogger
                .When(logger => logger.LogError((Arg.Any<string>())))
                .Do(info => throw new Exception("fake exception"));
            var analyzer = new LogAnalyzer3(stubLogger, mockWebService) { MinNameLength = 10 };

            analyzer.Analyze("Short.txt");

            mockWebService.Received()
                .Write(Arg.Is<ErrorInfo>(info => info.Severity == 1000 &&
                                                 info.Message.Contains("fake exception")));
        }
    }
}
