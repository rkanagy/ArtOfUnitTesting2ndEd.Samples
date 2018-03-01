using NUnit.Framework;

namespace MyLogAn3.UnitTests
{
    [TestFixture]
    public class XmlStringParserTests : TemplateStringParserTests
    {
        private IStringParser GetParser(string input)
        {
            return new XmlStringParser(input);
        }

        [Test]
        public override void TestGetStringVersionFromHeader_SingleDigit_Found()
        {
            var parser = GetParser("<Header>1</Header>");

            var versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1", versionFromHeader);
        }

        [Test]
        public override void TestGetStringVersionFromHeader_WithMinorVersion_Found()
        {
            var parser = GetParser("<Header>1.1</Header>");

            var versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1.1", versionFromHeader);
        }

        [Test]
        public override void TestGetStringVersionFromHeader_WithRevision_Found()
        {
            var parser = GetParser("<Header>1.1.1</Header>");

            var versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1.1.1", versionFromHeader);
        }
    }
}
