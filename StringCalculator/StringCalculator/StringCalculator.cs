namespace StringCalculator
{
    public class StringCalculator
    {
        private readonly Adder _adder = new Adder(new LessThanMaxNumberFilter(1000));
        private readonly IValidate _validator = new NegativeNumberValidator();
        private readonly Parser _parser = new Parser();

        public int Add(string input)
        {
            if (input.Length == 0)
            {
                return 0;
            }

            return AddCore(input);
        }

        private int AddCore(string input)
        {
            var numbers = _parser.GetNumbers(input);
            _validator.Validate(numbers);
            return _adder.Sum(numbers);
        }
    }
}