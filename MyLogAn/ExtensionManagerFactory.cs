namespace MyLogAn
{
    public static class ExtensionManagerFactory
    {
        private static IExtensionManager _customManager = null;

        public static IExtensionManager Create()
        {
            return _customManager ?? new FileExtensionManager();
        }

        public static void SetManager(IExtensionManager mgr)
        {
            _customManager = mgr;
        }
    }
}
