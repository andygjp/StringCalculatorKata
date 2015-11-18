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
        [Fact]
        public void It_should_return_zero()
        {
            var sut = new StringCalculator();
            int actual = sut.Add("1,2");
            actual.Should().Be(3);
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
            var numbers = input.Split(',');
            return numbers.Select(n => int.Parse(n, NumberStyles.Integer)).Sum();
        }
    }
}
