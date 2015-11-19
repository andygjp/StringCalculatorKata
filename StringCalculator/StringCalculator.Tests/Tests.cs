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

    public class When_I_supply_input_that_contains_negative_numbers
    {
        [Theory]
        [InlineData("-1", "negatives not allowed: -1")]
        [InlineData("-3", "negatives not allowed: -3")]
        [InlineData("-3,-1", "negatives not allowed: -3,-1")]
        public void It_should_throw_an_exception(string input, string expectedMessage)
        {
            Action call = () => new StringCalculator().Add(input);
            call.ShouldThrowExactly<ArgumentException>().WithMessage(expectedMessage);
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
            var parsed = Parse(input).ToList();
            Validate(parsed);
            return parsed.Sum();
        }

        private static void Validate(IEnumerable<int> parsed)
        {
            var negative = GetNegativeNumbers(parsed);
            if (AreThereNegativeNumbers(negative))
            {
                return;
            }
            ThrowException(negative);
        }

        private static void ThrowException(IEnumerable<int> negative)
        {
            throw new ArgumentException("negatives not allowed: " + JoinNumbers(negative));
        }

        private static string JoinNumbers(IEnumerable<int> negative)
        {
            return negative.Aggregate("", Concat).TrimEnd(',');
        }

        private static string Concat(string current, int x)
        {
            return $"{current}{x},";
        }

        private static bool AreThereNegativeNumbers(IEnumerable<int> negative)
        {
            return !negative.Any();
        }

        private static List<int> GetNegativeNumbers(IEnumerable<int> parsed)
        {
            return parsed.Where(x => x < 0).ToList();
        }

        private static IEnumerable<int> Parse(string input)
        {
            return Split(input).Select(int.Parse);
        }

        private static IEnumerable<string> Split(string input)
        {
            var separators = GetSeparators(input);
            input = RemoveCustomSeparator(input);
            return input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string RemoveCustomSeparator(string input)
        {
            return IsSeparatorSpecified(input) ? input.Substring(3) : input;
        }

        private static char[] GetSeparators(string input)
        {
            var separator = GetCustomSeparator(input);
            return new[] {separator, '\n'};
        }

        private static char GetCustomSeparator(string input)
        {
            return IsSeparatorSpecified(input) ? input[2] : ',';
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
