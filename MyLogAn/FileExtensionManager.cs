using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace MyLogAn
{
    public class FileExtensionManager : IExtensionManager
    {
        public bool IsValid(string fileName)
        {
            ValidateFileName(fileName);

            return CheckFileExtension(fileName, GetSupportedExtensionsCollection());
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void ValidateFileName(string fileName)
        {
            if (fileName == string.Empty)
            {
                throw new ArgumentException("filename has to be provided");
            }
        }

        private static IEnumerable<string> GetSupportedExtensionsCollection()
        {
            // STEP 1: READ THROUGH THE CONFIGURATION FILE FOR SUPPORTED EXTENSIONS
            // Here we have a dependency on the file system, which we will 
            // need to break/remove.
            var supportedExtensions = GetSupportedExtensions();
            var arrSupportedExtensions = supportedExtensions.Split(',');

            return arrSupportedExtensions.ToList();
        }

        private static bool CheckFileExtension(string fileName, IEnumerable<string> supportedExtensions)
        {
            // STEP 2:  RETURN TRUE IF CONFIGURATION FILE SAYS EXTENSION IS SUPPORTED
            return supportedExtensions.Any(extension => 
                fileName.EndsWith(extension, StringComparison.CurrentCultureIgnoreCase));
        }

        private static string GetSupportedExtensions()
        {
            var appSettings = OpenConfigFileAppSettingsSection();

            return appSettings.Settings["SupportedExtensions"].Value;
        }

        private static AppSettingsSection OpenConfigFileAppSettingsSection()
        {
            var config = OpenConfigFile();
            ConfigurationManager.RefreshSection("appSettings");

            return config.AppSettings;
        }

        private static Configuration OpenConfigFile()
        {
            var configFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = GetConfigFileFullPath()
            };

            return ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
        }

        private static string GetConfigFileFullPath()
        {
            var configFilePath = AppDomain.CurrentDomain.BaseDirectory;
            var configFileName = GetConfigFileName();
            var configFileFullPath = configFilePath + configFileName;

            return configFileFullPath;
        }

        private static string GetConfigFileName()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            var assemblyNameProperty = assemblyName.Name;

            return assemblyNameProperty + ".dll.config";
        }
    }
}
