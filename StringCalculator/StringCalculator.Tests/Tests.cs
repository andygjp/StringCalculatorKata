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

    public class StringCalculator
    {
        public int Add(string input)
        {
            throw new System.NotImplementedException();
        }
    }
}
