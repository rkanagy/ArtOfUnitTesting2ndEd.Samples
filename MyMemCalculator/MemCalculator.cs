namespace MyMemCalculator
{
    public class MemCalculator
    {
        private int _sum = 0;

        public void Add(int number)
        {
            _sum += number;
        }

        public int Sum()
        {
            var temp = _sum;
            _sum = 0;
            return temp;
        }
    }
}
