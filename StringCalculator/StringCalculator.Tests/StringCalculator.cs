namespace StringCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    internal interface IFilter
    {
        IEnumerable<int> Filter(IEnumerable<int> ns);
    }

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

    internal class Adder
    {
        private readonly IFilter _filter;

        public Adder(int maxNumber)
        {
            _filter = new LessThanMaxNumberFilter(maxNumber);
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

    internal interface IValidate
    {
        void Validate(string[] numbers);
    }

    internal class NegativeNumberValidator : IValidate
    {
        public void Validate(string[] numbers)
        {
            var negativeNumbers = GetNegativeNumbers(numbers);
            if (negativeNumbers.Any())
            {
                var message = GetErrorMessage(negativeNumbers);
                throw new ArgumentException(message);
            }
        }

        private static List<string> GetNegativeNumbers(IEnumerable<string> numbers)
        {
            var negativeNumbers = numbers.Where(s => s.StartsWith("-")).ToList();
            return negativeNumbers;
        }

        private static string GetErrorMessage(IEnumerable<string> negativeNumbers)
        {
            return negativeNumbers.Aggregate("negatives not allowed: ", (current, next) => $"{current}{next},").TrimEnd(',');
        }
    }

    internal class Parser
    {
        public string[] GetNumbers(string input)
        {
            char separator = ',';
            if (input.StartsWith("//") && input.Length > 2)
            {
                separator = input[2];
                input = input.Substring(3);
            }
            return Split(input, separator);
        }

        private static string[] Split(string input, char separator)
        {
            var numbers = input.Split(new[] {'\n', separator}, StringSplitOptions.RemoveEmptyEntries);
            return numbers;
        }
    }

    public class StringCalculator
    {
        private readonly Adder _adder = new Adder(1000);
        private readonly IValidate _validator = new NegativeNumberValidator();
        private readonly Parser _parser = new Parser();

        public int Add(string input)
        {
            if (input.Length == 0)
            {
                return 0;
            }

            return AddCore(input);
        }

        private int AddCore(string input)
        {
            var numbers = _parser.GetNumbers(input);
            _validator.Validate(numbers);
            return _adder.Sum(numbers);
        }
    }
}