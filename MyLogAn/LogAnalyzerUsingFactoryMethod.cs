namespace MyLogAn
{
    public class LogAnalyzerUsingFactoryMethod
    {
        public bool IsValidLogFileName(string fileName)
        {
            var mgr = GetManager();

            // determine which of the two factory methods to use
            return mgr?.IsValid(fileName) ?? IsValid(fileName);
        }

        // using factory method to return a result
        protected virtual bool IsValid(string fileName)
        {
            var mgr = new FileExtensionManager();
            return mgr.IsValid(fileName);
        }

        // using a factory method to create instance of extension manager
        protected virtual IExtensionManager GetManager()
        {
            return new FileExtensionManager();
        }
    }
}
