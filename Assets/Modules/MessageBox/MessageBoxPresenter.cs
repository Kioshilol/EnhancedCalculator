using System;

namespace MessageBox
{
    public class MessageBoxPresenter
    {
        private readonly IMessageBoxView _view;
        private Action _onClosed;

        public MessageBoxPresenter(IMessageBoxView view)
        {
            _view = view;
            _view.OnConfirmClicked += HandleConfirm;
        }

        public void Show(
            string message, 
            Action onClosed)
        {
            _onClosed = onClosed;
            _view.SetMessage(message);
            _view.SetVisible(true);
        }

        public void Hide()
        {
            _view.SetVisible(false);
        }

        private void HandleConfirm()
        {
            _view.SetVisible(false);
            _onClosed?.Invoke();
            _onClosed = null;
        }

        public void Dispose()
        {
            _view.OnConfirmClicked -= HandleConfirm;
        }
    }
}
