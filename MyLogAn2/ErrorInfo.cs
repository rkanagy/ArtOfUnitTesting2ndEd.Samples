namespace MyLogAn2
{
    public class ErrorInfo
    {
        public ErrorInfo(int severity, string message)
        {
            Severity = severity;
            Message = message;
        }

        public int Severity { get; }
        public string Message { get; }
    }
}
