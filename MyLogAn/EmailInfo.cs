namespace MyLogAn
{
    public class EmailInfo
    {
        private readonly string _body;
        private readonly string _to;
        private readonly string _subject;

        public EmailInfo(string body, string to, string subject)
        {
            _body = body;
            _to = to;
            _subject = subject;
        }

        public override bool Equals(object other)
        {
            if (!(other is EmailInfo toCompareTo)) return false;

            return _body == toCompareTo._body && _to == toCompareTo._to && _subject == toCompareTo._subject;
        }

        public override int GetHashCode()
        {
            var body = _body;
            var to = _to;
            var subject = _subject;
            return new { body, to, subject }.GetHashCode();
        }
    }
}
