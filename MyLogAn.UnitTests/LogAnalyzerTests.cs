using NUnit.Framework;
using System;
using NSubstitute;

namespace MyLogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        private LogAnalyzer _mAnalyzer;

        [SetUp]
        public void Setup()
        {
            // be sure to use default extension manager with the file system dependency
            ExtensionManagerFactory.SetManager(null);
            _mAnalyzer = new LogAnalyzer();
        }

        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            var result = _mAnalyzer.IsValidLogFileName("filewithbadextension.foo");

            Assert.False(result);
        }

        [Test]
        [Category("Fast Tests")]
        //[Ignore("there is a problem with this test")]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            var result = _mAnalyzer.IsValidLogFileName("filewithgoodextension.slf");

            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            var result = _mAnalyzer.IsValidLogFileName("filewithgoodextension.SLF");
            
            Assert.True(result);
        }

        [TestCase("filewithgoodextension.SLF")]
        [TestCase("filewithgoodextension.slf")]
        public void IsValidLogFileName_ValidExtensions_ReturnsTrue(string file)
        {
            var result = _mAnalyzer.IsValidLogFileName(file);

            Assert.True(result);
        }

        [TestCase("filewithgoodextension.SLF", true)]
        [TestCase("filewithgoodextension.slf", true)]
        [TestCase("filewithbadextension.foo", false)]
        public void IsValidLogFileName_VariousExtensions_ChecksThem(string file, bool expected)
        {
            var result = _mAnalyzer.IsValidLogFileName(file);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_ThrowsException()
        {
            var analyzer = MakeAnalyzer();

            Assert.Throws<ArgumentException>(() => analyzer.IsValidLogFileName(string.Empty),
                "filename has to be provided");
        }

        [Test]
        public void IsValidFileName_EmptyFileName_Throws()
        {
            var analyzer = MakeAnalyzer();

            var ex = Assert.Catch<ArgumentException>(() => analyzer.IsValidLogFileName(string.Empty));

            StringAssert.Contains("filename has to be provided", ex.Message);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_ThrowsFluent()
        {
            var analyzer = MakeAnalyzer();

            var ex = Assert.Catch<ArgumentException>(() => analyzer.IsValidLogFileName(string.Empty));

            Assert.That(ex.Message, Does.Contain("filename has to be provided"));
        }

        [Test]
        public void IsValidFileName_WhenCalled_ChangeWasLastFileNameValid()
        {
            var analyzer = MakeAnalyzer();

            analyzer.IsValidLogFileName("badname.foo");

            Assert.False(analyzer.WasLastFileNameValid);
        }

        [TestCase("badfile.foo", false)]
        [TestCase("goodfile.slf", true)]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected)
        {
            var analyzer = MakeAnalyzer();

            analyzer.IsValidLogFileName(file);

            Assert.AreEqual(expected, analyzer.WasLastFileNameValid);
        }

        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            var myFakeManager = new FakeExtensionManager { WillBeValid = true };
            var logAnalyzerDeps = new LogAnalyzerDependencies { ExtensionManager = myFakeManager };

            // using constructor injection with parameter object refactoring
            var log = new LogAnalyzer(logAnalyzerDeps);

            var result = log.IsValidLogFileName("short.ext");
            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            var myFakeManager = new FakeExtensionManager { WillThrow = new Exception("this is fake") };

            // using constructor injection
            var log = new LogAnalyzer(myFakeManager);

            var result = log.IsValidLogFileName("anything.anyextension");
            Assert.False(result);
        }

        [Test]
        public void IsValidFileName_SupportedExtension_ReturnsTrue()
        {
            var myFakeManager = new FakeExtensionManager { WillBeValid = true };

            // using parameter injection
            var log = new LogAnalyzer(myFakeManager) { ExtensionManager = myFakeManager };

            var result = log.IsValidLogFileName("anything.anyextension");
            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_SupportedExtensionWithFactory_ReturnsTrue()
        {
            // using factory class for injection
            var myFakeManager = new FakeExtensionManager { WillBeValid = true };
            ExtensionManagerFactory.SetManager(myFakeManager);

            var log = new LogAnalyzer();

            var result = log.IsValidLogFileName("anything.anyextension");
            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_SupportsExtensionWithFactoryInterface_ReturnsTrue()
        {
            // using factory class with interface for constructor injection for factory
            var myFakeManagerFactory = new FakeExtensionManagerFactory { WillBeValid = true };

            var log = new LogAnalyzer(myFakeManagerFactory);

            var result = log.IsValidLogFileName("anything.anyextension");
            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_SupportsExtensionWithFactoryMethod_ReturnsTrue()
        {
            var myFakeManager = new FakeExtensionManager { WillBeValid = true };

            // using factory method to create our fake extension manager 
            // (Extract and Override pattern from Working Effectively with Legacy Code by Michael C. Feathers)
            // TODO: read and study the book Working Effectively with Legacy Code by Michael C. Feathers
            var log = new TestableLogAnalyzer(myFakeManager);

            var result = log.IsValidLogFileName("anything.anyextension");
            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_SupportsExtensionWithFactoryMethodReturningResult_ReturnsTrue()
        {
            var log = new TestableLogAnalyzer { IsSupported = true };

            var result = log.IsValidLogFileName("file.ext");
            Assert.True(result, "...");
        }

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            var mockService = new FakeWebService();
            var log = new LogAnalyzer(mockService) { MinNameLength = 8 };
            const string tooShortFileName = "abc.ext";

            log.Analyze(tooShortFileName);

            StringAssert.Contains("Filename too short: abc.ext", mockService.LastError);
        }

        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            var stubService = new FakeWebService { ToThrow = new Exception("fake exception") };
            var mockEmail = new FakeEmailService();
            var log = new LogAnalyzer(stubService, mockEmail) { MinNameLength = 8 };
            const string tooShortFileName = "abc.ext";

            log.Analyze(tooShortFileName);

            var expectedEmail = new EmailInfo("fake exception", "someone@somewhere.com", "can't log");
            Assert.AreEqual(expectedEmail, mockEmail.Email);
        }

        [Test]
        public void Analyze_TooShortFileName_CallLogger()
        {
            var logger = Substitute.For<ILogger>();
            var analyzer = new LogAnalyzer(logger) { MinNameLength = 6 };

            analyzer.Analyze("a.txt");

            logger.Received().LogError("Filename too short: a.txt");
        }

        private static LogAnalyzer MakeAnalyzer()
        {
            // be sure to use default extension manager with the file system dependency
            ExtensionManagerFactory.SetManager(null);
            return new LogAnalyzer();
        }

        [TearDown]
        public void TearDown()
        {
            // the line below is included to show an anti pattern.
            // This isn't really needed.  Don't do it in real life.
            _mAnalyzer = null;
        }
    }

    internal class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid;
        public Exception WillThrow;

        public bool IsValid(string filename)
        {
            if (WillThrow != null)
            {
                throw WillThrow;
            }

            return WillBeValid;
        }
    }

    public class FakeExtensionManagerFactory : IExtensionManagerFactory
    {
        private readonly FakeExtensionManager _manager;

        public FakeExtensionManagerFactory()
        {
            _manager = new FakeExtensionManager();
        }

        public IExtensionManager Create()
        {
            _manager.WillBeValid = WillBeValid;
            return _manager;
        }

        public bool WillBeValid;
    }

    public class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
    {
        private readonly IExtensionManager _manager;

        public bool IsSupported;

        public TestableLogAnalyzer()
        {
            _manager = null;
        }

        public TestableLogAnalyzer(IExtensionManager mgr)
        {
            _manager = mgr;
        }

        protected override bool IsValid(string fileName)
        {
            return IsSupported;
        }

        protected override IExtensionManager GetManager()
        {
            return _manager;
        }
    }

    // This type of test clas is called a Test Spy,
    // according to xUnit Test Patterns:  Refactoring Test Code by Gerard Meszaros
    public class FakeWebService : IWebService
    {
        public string LastError;
        public Exception ToThrow;

        public void LogError(string message)
        {
            LastError = message;
            if (ToThrow != null)
            {
                throw ToThrow;
            }
        }
    }

    public class FakeEmailService : IEmailService
    {
        public EmailInfo Email;

        public void SendEmail(EmailInfo emailInfo)
        {
            Email = emailInfo;
        }
    }
}
