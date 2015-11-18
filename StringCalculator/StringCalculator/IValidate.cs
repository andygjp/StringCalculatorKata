namespace StringCalculator
{
    using System.Collections.Generic;

    internal interface IValidate
    {
        void Validate(IEnumerable<string> numbers);
    }
}