namespace MyLogAn3
{
    // This class uses the LoggingFacility internally
    public class LogAnalyzer
    {
        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                LoggingFacility.Log("Filename too short: " + fileName);
            }
            // rest of the method here
        }
    }
}
