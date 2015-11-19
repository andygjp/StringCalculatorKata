namespace StringCalculator.Tests
{
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

    public class StringCalculator
    {
        public int Add(string input)
        {
            if (IsInputBlank(input))
            {
                input = "0";
            }
            return int.Parse(input);
        }

        private static bool IsInputBlank(string input)
        {
            return input.Length == 0;
        }
    }
}
