namespace StringCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    internal class Adder
    {
        private readonly int _maxNumber;

        public Adder(int maxNumber)
        {
            _maxNumber = maxNumber;
        }

        public int Sum(string[] numbers)
        {
            return numbers.Select(n => int.Parse(n, NumberStyles.Integer)).Where(n => n <= _maxNumber).Sum();
        }
    }

    internal class Validator
    {
        public void Validate(string[] numbers)
        {
            var negativeNumbers = numbers.Where(s => s.StartsWith("-")).ToList();
            if (negativeNumbers.Any())
            {
                var message = GetErrorMessage(negativeNumbers);
                throw new ArgumentException(message);
            }
        }

        private static string GetErrorMessage(IEnumerable<string> negativeNumbers)
        {
            return negativeNumbers.Aggregate("negatives not allowed: ", (current, next) => $"{current}{next},").TrimEnd(',');
        }
    }

    public class StringCalculator
    {
        private readonly Adder _adder = new Adder(1000);
        private readonly Validator _validator = new Validator();

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
            var numbers = GetNumbers(input);
            _validator.Validate(numbers);
            return _adder.Sum(numbers);
        }

        private static string[] GetNumbers(string input)
        {
            char separator = ',';
            if (input.StartsWith("//") && input.Length > 2)
            {
                separator = input[2];
                input = input.Substring(3);
            }
            var numbers = input.Split(new[] {'\n', separator}, StringSplitOptions.RemoveEmptyEntries);
            return numbers;
        }
    }
}