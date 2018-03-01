using System;

namespace MyLogAn3
{
    public class StandardStringParser
    {
        private readonly string _input;

        public StandardStringParser(string input)
        {
            _input = input;
        }

        public string GetStringVersionFromHeader()
        {
            const string versionTag = "version=";
            const string delimiter = ";";

            var versionIdx = _input.IndexOf(versionTag, StringComparison.Ordinal);
            var delimiterIdx = _input.IndexOf(delimiter, versionIdx, StringComparison.Ordinal);
            var versionStart = versionIdx + versionTag.Length;
            var version = _input.Substring(versionStart, delimiterIdx - versionStart);

            return version;
        }
    }
}
