using System.Xml;

namespace MyLogAn3
{
    public class XmlStringParser : BaseStringParser
    {
        public XmlStringParser(string stringToParse) : base(stringToParse)
        {
            // nothing to do
        }

        public override bool HasCorrectHeader()
        {
            // not implemented
            return false;
        }

        public override string GetStringVersionFromHeader()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(StringToParse);
            var version = xmlDoc.SelectSingleNode("//Header")?.InnerText;

            return version;
        }
    }
}
