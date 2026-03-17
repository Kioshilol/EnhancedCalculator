using Calculator.Domain;
using Calculator.Data;
using MessageBox;

namespace Calculator.Presentation
{
    public class CalculatorPresenter
    {
        private readonly ICalculatorView _view;
        private readonly ICalculator _calculator;
        private readonly ICalculatorRepository _repository;
        private readonly IMessageBoxService _messageBox;
        private readonly CalculatorState _state;

        public CalculatorPresenter(
            ICalculatorView view,
            ICalculator calculator,
            ICalculatorRepository repository,
            IMessageBoxService messageBox)
        {
            _view = view;
            _calculator = calculator;
            _repository = repository;
            _messageBox = messageBox;

            _state = _repository.Load();
            RestoreState();

            _view.OnResultClicked += HandleResult;
        }

        private void RestoreState()
        {
            _view.InputText = _state.CurrentInput;
            _view.SetHistory(_state.History);
        }

        private void HandleResult()
        {
            var input = _view.InputText.Trim();
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            var result = _calculator.Calculate(input);
            var entry = result.DisplayText;

            _state.History.Insert(0, entry);
            _view.AddHistoryEntry(entry);
            _view.InputText = string.Empty;
            _state.CurrentInput = string.Empty;

            if (!result.IsSuccess)
            {
                var lastInput = input;
                _messageBox.Show("Please check the expression\nyou just entered", () =>
                {
                    _view.InputText = lastInput;
                    _state.CurrentInput = lastInput;
                    SaveState();
                });
            }

            SaveState();
        }

        public void OnInputChanged(string text)
        {
            _state.CurrentInput = text;
            SaveState();
        }

        private void SaveState()
        {
            _repository.Save(_state);
        }

        public void Dispose()
        {
            _view.OnResultClicked -= HandleResult;
        }
    }
}
