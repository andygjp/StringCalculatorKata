namespace StringCalculator
{
    using System;
    using System.Globalization;
    using System.Linq;

    public class StringCalculator
    {
        public int Add(string input)
        {
            if (input.Length == 0)
            {
                return 0;
            }

            return AddCore(input);
        }

        private static int AddCore(string input)
        {
            var numbers = GetNumbers(input);
            Validate(numbers);
            return Sum(numbers);
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

        private static void Validate(string[] numbers)
        {
            var negativeNumbers = numbers.Where(s => s.StartsWith("-")).ToList();
            if (negativeNumbers.Any())
            {
                var message =
                    negativeNumbers.Aggregate("negatives not allowed: ", (current, next) => $"{current}{next},").TrimEnd(',');
                throw new ArgumentException(message);
            }
        }

        private static int Sum(string[] numbers)
        {
            return numbers.Select(n => int.Parse(n, NumberStyles.Integer)).Where(n => n <= 1000).Sum();
        }
    }
}