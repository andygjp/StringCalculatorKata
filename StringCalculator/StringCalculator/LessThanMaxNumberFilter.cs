namespace StringCalculator
{
    using System.Collections.Generic;
    using System.Linq;

    internal class LessThanMaxNumberFilter : IFilter
    {
        private readonly int _maxNumber;

        public LessThanMaxNumberFilter(int maxNumber)
        {
            _maxNumber = maxNumber;
        }

        public IEnumerable<int> Filter(IEnumerable<int> ns)
        {
            return ns.Where(n => n <= _maxNumber);
        }
    }
}