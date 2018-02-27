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

            var unused = new Presenter(mockView);
            mockView.Loaded += Raise.Event<Action>();

            mockView.Received()
                .Render(Arg.Is<string>(s => s.Contains("Hello World")));
        }
    }
}
