using Infrastructure.Data;
using Calculator.Domain;

namespace Calculator.Data
{
    public class CalculatorRepository : BaseRepository<CalculatorState>, ICalculatorRepository
    {
        protected override string Key => "CalculatorState";
    }
}
