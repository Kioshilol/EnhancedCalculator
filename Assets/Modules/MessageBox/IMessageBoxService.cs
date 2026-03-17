using System;

namespace MessageBox
{
    public interface IMessageBoxService
    {
        void Show(string message, string buttonText, Action onClosed);
        void Hide();
    }
}
