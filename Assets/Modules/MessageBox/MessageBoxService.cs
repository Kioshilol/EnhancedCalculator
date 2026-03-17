using System;

namespace MessageBox
{
    public class MessageBoxService : IMessageBoxService
    {
        private readonly MessageBoxPresenter _presenter;

        public MessageBoxService(MessageBoxPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Show(
            string message, 
            Action onClosed)
        {
            _presenter.Show(message, onClosed);
        }

        public void Hide()
        {
            _presenter.Hide();
        }
    }
}
