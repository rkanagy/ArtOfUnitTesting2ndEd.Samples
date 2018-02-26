namespace MyLogAn2
{
    public class LogAnalyzer
    {
        private readonly ILogger _logger;

        public int MinNameLength;

        public LogAnalyzer(ILogger logger)
        {
            _logger = logger;
        }

        public void Analyze(string fileName)
        {
            if (fileName.Length < MinNameLength)
            {
                _logger.LogError($"Filename too short: {fileName}");
            }

        }
    }
}
