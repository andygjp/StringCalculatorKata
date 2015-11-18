namespace StringCalculator
{
    using System;

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
}