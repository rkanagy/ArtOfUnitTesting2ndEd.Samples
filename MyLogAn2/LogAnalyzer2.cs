using System;

namespace MyLogAn2
{
    public class LogAnalyzer2
    {
        private readonly ILogger _logger;
        private readonly IWebService _webService;

        public LogAnalyzer2(ILogger logger, IWebService webService)
        {
            _logger = logger;
            _webService = webService;
        }

        public int MinNameLength { private get; set; }

        public void Analyze(string fileName)
        {
            if (fileName.Length < MinNameLength)
            {
                try
                {
                    _logger.LogError($"Filename too short: {fileName}");
                }
                catch (Exception ex)
                {
                    _webService.Write("Error From Logger: " + ex);
                }
            }
        }
    }
}
