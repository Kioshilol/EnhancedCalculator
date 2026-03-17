using System;
using System.Collections.Generic;

namespace Calculator.Domain
{
    [Serializable]
    public class CalculatorState
    {
        public string CurrentInput;
        public List<string> History;

        public CalculatorState()
        {
            CurrentInput = string.Empty;
            History = new List<string>();
        }
    }
}
