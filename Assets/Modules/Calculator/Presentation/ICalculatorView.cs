using System;
using System.Collections.Generic;

namespace Calculator.Presentation
{
    public interface ICalculatorView
    {
        event Action OnResultClicked;
        string InputText { get; set; }
        void SetHistory(List<string> history);
        void AddHistoryEntry(string entry);
    }
}
