namespace StringCalculator.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class When_I_supply_blank_input
    {
        [Fact]
        public void It_should_return_zero()
        {
            var sut = new StringCalculator();
            int actual = sut.Add("");
            actual.Should().Be(0);
        }
    }

    public class When_I_supply_single_number
    {
        [Fact]
        public void It_should_return_that_value()
        {
            var sut = new StringCalculator();
            int actual = sut.Add("1");
            actual.Should().Be(1);
        }
    }

    public class When_I_supply_two_numbers
    {
        [Fact]
        public void It_should_sum_those_numbers()
        {
            var sut = new StringCalculator();
            int actual = sut.Add("1,2");
            actual.Should().Be(3);
        }
    }

    public class When_I_supply_many_numbers
    {
        [Fact]
        public void It_should_sum_those_numbers()
        {
            var sut = new StringCalculator();
            int actual = sut.Add("1,2,3,4,5,6,7,8");
            actual.Should().Be(36);
        }
    }

    public class When_I_supply_many_numbers_that_are_separated_with_newline
    {
        [Fact]
        public void It_should_sum_those_numbers()
        {
            var sut = new StringCalculator();
            int actual = sut.Add("1\n2,3,4\n5,6,7,8");
            actual.Should().Be(36);
        }
    }

    public class When_I_supply_many_numbers_that_have_a_custom_separator
    {
        [Fact]
        public void It_should_sum_those_numbers()
        {
            var sut = new StringCalculator();
            int actual = sut.Add("//;\n1;2;3;4;5;6;7;8");
            actual.Should().Be(36);
        }
    }

    public class StringCalculator
    {
        public int Add(string input)
        {
            input = SanitiseInput(input);
            return Sum(input);
        }

        private static int Sum(string input)
        {
            var parsed = Parse(input);
            return parsed.Sum();
        }

        private static IEnumerable<int> Parse(string input)
        {
            var split = Split(input);
            return split.Select(int.Parse);
        }

        private static IEnumerable<string> Split(string input)
        {
            return input.Split(GetSeparators(input), StringSplitOptions.RemoveEmptyEntries);
        }

        private static char[] GetSeparators(string input)
        {
            var separator = GetCustomSeparator(input);
            return new[] {separator, '\n'};
        }

        private static char GetCustomSeparator(string input)
        {
            var separator = ',';
            if (IsSeparatorSpecified(input))
            {
                separator = input[2];
            }
            return separator;
        }

        private static bool IsSeparatorSpecified(string input)
        {
            return input.StartsWith("//") && input.Length > 2;
        }

        private static string SanitiseInput(string input)
        {
            return IsInputBlank(input) ? "0" : input;
        }

        private static bool IsInputBlank(string input)
        {
            return input.Length == 0;
        }
    }
}
