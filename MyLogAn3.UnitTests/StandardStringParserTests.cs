using NUnit.Framework;

namespace MyLogAn3.UnitTests
{
    [TestFixture]
    public class StandardStringParserTests
    {
        private StandardStringParser GetParser(string input)
        {
            return new StandardStringParser(input);
        }

        [Test]
        public void GetStringVersionFromHeader_SingleDigit_Found()
        {
            const string input = "header;version=1;\n";
            var parser = GetParser(input);

            var versionFromHeader = parser.GetStringVersionFromHeader();
            Assert.AreEqual("1", versionFromHeader);
        }

        [Test]
        public void GetStringVersionFromHeader_WithMinorVersion_Found()
        {
            const string input = "header;version=1.1;\n";
            var parser = GetParser(input);

            var versionFromHeader = parser.GetStringVersionFromHeader();
            Assert.AreEqual("1.1", versionFromHeader);
        }

        [Test]
        public void GetStringVersionFromHeader_WithRevision_Found()
        {
            const string input = "header;version=1.1.1;\n";
            var parser = GetParser(input);

            var versionFromHeader = parser.GetStringVersionFromHeader();
            Assert.AreEqual("1.1.1", versionFromHeader);
        }
    }
}
