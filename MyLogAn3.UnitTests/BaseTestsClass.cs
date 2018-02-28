using NSubstitute;
using NUnit.Framework;

namespace MyLogAn3.UnitTests
{
    [TestFixture]
    public class BaseTestsClass
    {
        public ILogger FakeTheLogger()
        {
            LoggingFacility.Logger = Substitute.For<ILogger>();
            return LoggingFacility.Logger;
        }

        [TearDown]
        public void Teardown()
        {
            // need to reset a static resource between tests
            LoggingFacility.Logger = null;
        }
    }
}
