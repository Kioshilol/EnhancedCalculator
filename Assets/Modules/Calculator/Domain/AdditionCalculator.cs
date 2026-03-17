using System.Text.RegularExpressions;

namespace Calculator.Domain
{
    public class AdditionCalculator : ICalculator
    {
        private static readonly Regex ValidExpression = new(@"^\d+\+\d+$");

        public CalculationResult Calculate(string input)
        {
            if (string.IsNullOrEmpty(input) || !ValidExpression.IsMatch(input))
            {
                return CalculationResult.Error(input ?? string.Empty);
            }

            var parts = input.Split('+');
            if (parts.Length != 2 ||
                !long.TryParse(parts[0], out var a) ||
                !long.TryParse(parts[1], out var b))
            {
                return CalculationResult.Error(input);
            }

            return CalculationResult.Success(input, a + b);
        }
    }
}
