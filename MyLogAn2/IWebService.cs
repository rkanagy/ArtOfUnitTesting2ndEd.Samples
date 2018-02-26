namespace MyLogAn2
{
    public interface IWebService
    {
        void Write(string message);
        void Write(ErrorInfo errorInfo);
    }
}
