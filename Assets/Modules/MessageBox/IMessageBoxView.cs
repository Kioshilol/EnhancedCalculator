using System;

namespace MessageBox
{
    public interface IMessageBoxView
    {
        event Action OnConfirmClicked;
        void SetMessage(string message);
        void SetVisible(bool visible);
    }
}
