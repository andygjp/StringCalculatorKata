namespace StringCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class NegativeNumberValidator : IValidate
    {
        public void Validate(IEnumerable<string> numbers)
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
}