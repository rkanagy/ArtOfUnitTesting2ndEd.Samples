using System;

namespace MyLogAn3
{
    public class StandardStringParser : BaseStringParser
    {
        public StandardStringParser(string input) : base(input)
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
            const string versionTag = "version=";
            const string delimiter = ";";

            var versionIdx = StringToParse.IndexOf(versionTag, StringComparison.Ordinal);
            var delimiterIdx = StringToParse.IndexOf(delimiter, versionIdx, StringComparison.Ordinal);
            var versionStart = versionIdx + versionTag.Length;
            var version = StringToParse.Substring(versionStart, delimiterIdx - versionStart);

            return version;
        }
    }
}
