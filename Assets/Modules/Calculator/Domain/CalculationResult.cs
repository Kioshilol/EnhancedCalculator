namespace Calculator.Domain
{
    public class CalculationResult
    {
        public bool IsSuccess { get; }
        public long Sum { get; }
        public string Expression { get; }
        public string DisplayText { get; }

        private CalculationResult(
            bool isSuccess,
            long sum,
            string expression)
        {
            IsSuccess = isSuccess;
            Sum = sum;
            Expression = expression;
            DisplayText = isSuccess ? $"{expression}={sum}" : $"{expression}=ERROR";
        }

        public static CalculationResult Success(string expression, long sum) 
            => new(true, sum, expression);

        public static CalculationResult Error(string expression) 
            => new(false, 0, expression);
    }
}
