namespace MyLogAn
{
    public class FileExtensionManagerFactory : IExtensionManagerFactory
    {
        public IExtensionManager Create()
        {
            return new FileExtensionManager();
        }
    }
}
