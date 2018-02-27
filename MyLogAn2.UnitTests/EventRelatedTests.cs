using System;
using NSubstitute;
using NUnit.Framework;

namespace MyLogAn2.UnitTests
{
    [TestFixture]
    public class EventRelatedTests
    {
        [Test]
        public void Ctor_WhenViewIsLoaded_CallsViewRender()
        {
            var mockView = Substitute.For<IView>();

            var unused = new Presenter(mockView, Substitute.For<ILogger>());
            mockView.Loaded += Raise.Event<Action>();

            mockView.Received()
                .Render(Arg.Is<string>(s => s.Contains("Hello World")));
        }

        [Test]
        public void Ctor_WhenViewHasError_CallsLogger()
        {
            var stubView = Substitute.For<IView>();
            var mockLogger = Substitute.For<ILogger>();

            var unused = new Presenter(stubView, mockLogger);
            stubView.ErrorOccurred += Raise.Event<Action<string>>("fake error");

            mockLogger.Received()
                .LogError(Arg.Is<string>(s => s.Contains("fake error")));
        }
    }
}
