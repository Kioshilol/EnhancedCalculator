using Infrastructure.Data;
using Calculator.Domain;

namespace Calculator.Data
{
    public interface ICalculatorRepository : IRepository<CalculatorState>
    {
    }
}
