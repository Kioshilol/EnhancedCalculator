namespace Calculator.Domain
{
    public interface ICalculator
    {
        CalculationResult Calculate(string input);
    }
}
