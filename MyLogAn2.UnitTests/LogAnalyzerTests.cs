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
    }
}
