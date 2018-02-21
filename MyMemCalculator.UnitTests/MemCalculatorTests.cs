using NUnit.Framework;

namespace MyMemCalculator.UnitTests
{
    [TestFixture]
    public class MemCalculatorTests
    {
        [Test]
        public void Sum_ByDefault_ReturnsZero()
        {
            var calc = MakeCalc();

            var lastSum = calc.Sum();

            Assert.AreEqual(0, lastSum);
        }

        [Test]
        public void Add_WhenCalled_ChangesSum()
        {
            var calc = MakeCalc();

            calc.Add(1);
            var sum = calc.Sum();

            Assert.AreEqual(1, sum);
        }

        private static MemCalculator MakeCalc()
        {
            return new MemCalculator();
        }
    }
}
