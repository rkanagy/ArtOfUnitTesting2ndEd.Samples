namespace MyLogAn3
{
    public abstract class BaseStringParser : IStringParser
    {
        public string StringToParse { get; }

        protected BaseStringParser(string stringToParse)
        {
            StringToParse = stringToParse;
        }

        public abstract bool HasCorrectHeader();
        public abstract string GetStringVersionFromHeader();
    }
}
