﻿namespace StringCalculator.Tests
{
    using System;
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
        public void It_should_return_input()
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
        public void It_should_return_sum_of_input(string input, int expected)
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
        public void It_should_return_sum_of_input(string input, int expected)
        {
            var sut = new StringCalculator();
            int actual = sut.Add(input);
            actual.Should().Be(expected);
        }
    }

    public class When_I_supply_many_numbers_using_a_custom_separator
    {
        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//+\n1+2+3+4", 10)]
        public void It_should_return_sum_of_input(string input, int expected)
        {
            var sut = new StringCalculator();
            int actual = sut.Add(input);
            actual.Should().Be(expected);
        }
    }

    public class When_I_supply_a_negative_numbers
    {
        [Theory]
        [InlineData("-1", "negatives not allowed: -1")]
        [InlineData("-1,2,-3,4", "negatives not allowed: -1,-3")]
        public void It_should_return_sum_of_input(string input, string expectedMessage)
        {
            var sut = new StringCalculator();
            Action call = () => sut.Add(input);
            call.ShouldThrowExactly<ArgumentException>().WithMessage(expectedMessage);
        }
    }

    public class When_I_supply_large_numbers
    {
        [Fact]
        public void It_should_ignore_numbers_greater_than_one_thousand()
        {
            var sut = new StringCalculator();
            int actual = sut.Add("999,1000,1001");
            actual.Should().Be(1999);
        }
    }
}
