namespace MyLogAn3
{
    // Another class that uses the LoggingFacility internally
    public class ConfigurationManager
    {
        public bool IsConfigured(string configName)
        {
            LoggingFacility.Log("checking " + configName);
            return true; // for demo purposes
        }
    }
}
