using System;

namespace MessageBox
{
    public interface IMessageBoxService
    {
        void Show(string message, Action onClosed);
        void Hide();
    }
}
