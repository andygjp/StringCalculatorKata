namespace StringCalculator
{
    using System.Collections.Generic;

    internal interface IFilter
    {
        IEnumerable<int> Filter(IEnumerable<int> ns);
    }
}