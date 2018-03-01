namespace MyLogAn3
{
    public interface IStringParser
    {
        string StringToParse { get; }

        bool HasCorrectHeader();
        string GetStringVersionFromHeader();
    }
}
