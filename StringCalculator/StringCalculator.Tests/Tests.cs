namespace StringCalculator.Tests
{
    using System.Globalization;
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
        public void It_should_return_zero()
        {
            var sut = new StringCalculator();
            int actual = sut.Add("1");
            actual.Should().Be(1);
        }
    }

    public class When_I_supply_many_numbers
    {
        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("1,2,3,4", 10)]
        public void It_should_return_zero(string input, int expected)
        {
            var sut = new StringCalculator();
            int actual = sut.Add(input);
            actual.Should().Be(expected);
        }
    }

    public class When_I_supply_many_numbers_that_contain_newline
    {
        [Theory]
        [InlineData("1\n2", 3)]
        [InlineData("1\n2,3,4", 10)]
        public void It_should_return_zero(string input, int expected)
        {
            var sut = new StringCalculator();
            int actual = sut.Add(input);
            actual.Should().Be(expected);
        }
    }

    public class StringCalculator
    {
        public int Add(string input)
        {
            if (input.Length == 0)
            {
                return 0;
            }
            var numbers = input.Split('\n', ',');
            return numbers.Select(n => int.Parse(n, NumberStyles.Integer)).Sum();
        }
    }
}
