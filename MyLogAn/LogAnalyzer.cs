using System;

namespace MyLogAn
{
    public class LogAnalyzer
    {
        private readonly IWebService _service;
        private readonly IEmailService _email;

        // using parameter injection
        public IExtensionManager ExtensionManager { private get; set; }

        public LogAnalyzer()
        {
            // using default extension manager with file system dependency
            //ExtensionManager = new FileExtensionManager();

            // using a factory class
            ExtensionManager = ExtensionManagerFactory.Create();
        }

        // TODO:  Investigate further into Dependency Injection (e.g. using Castle Windsor)
        // TODO:  Read Dependency Injection in .NET by Mark Seeman
        // Using constructor injection
        public LogAnalyzer(IExtensionManager mgr)
        {
            ExtensionManager = mgr;
        }

        // using constructor injection with parameter object refactoring
        public LogAnalyzer(LogAnalyzerDependencies deps)
        {
            ExtensionManager = deps.ExtensionManager;
        }

        // using constructor injection with factory class interface
        public LogAnalyzer(IExtensionManagerFactory factory)
        {
            ExtensionManager = factory.Create();
        }

        //  using constructor inject to inject web service
        public LogAnalyzer(IWebService service)
        {
            _service = service;
        }

        public LogAnalyzer(IWebService service, IEmailService email)
        {
            _service = service;
            _email = email;
        }

        public bool WasLastFileNameValid { get; private set; }
        
        public bool IsValidLogFileName(string fileName)
        {
            try
            {
                WasLastFileNameValid = ExtensionManager.IsValid(fileName);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                WasLastFileNameValid = false;
            }

            return WasLastFileNameValid;
        }

        public void Analyze(string fileName)
        {
            if (fileName.Length >= 8) return;

            try
            {
                _service.LogError("Filename too short: " + fileName);
            }
            catch (Exception ex)
            {
                var emailInfo = new EmailInfo(ex.Message, "someone@somewhere.com", "can't log");
                _email.SendEmail(emailInfo);
            }
        }
    }

    public class LogAnalyzerDependencies
    {
        public IExtensionManager ExtensionManager { get; set; }
    }
}
