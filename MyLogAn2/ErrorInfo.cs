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

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ErrorInfo) obj);
        }

        private bool Equals(ErrorInfo other)
        {
            return Severity == other.Severity && string.Equals(Message, other.Message);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Severity * 397) ^ Message.GetHashCode();
            }
        }
    }
}
