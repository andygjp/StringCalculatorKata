namespace StringCalculator
{
    using System.Globalization;
    using System.Linq;

    internal class Adder
    {
        private readonly IFilter _filter;

        public Adder(IFilter filter)
        {
            _filter = filter;
        }

        public int Sum(string[] numbers)
        {
            var ns = numbers.Select(ParseInteger);
            ns = _filter.Filter(ns);
            return ns.Sum();
        }

        private static int ParseInteger(string n)
        {
            return int.Parse(n, NumberStyles.Integer);
        }
    }
}