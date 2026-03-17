using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MessageBox
{
    public class MessageBoxView : MonoBehaviour, IMessageBoxView
    {
        [SerializeField]
        private GameObject _panel;

        [SerializeField]
        private TextMeshProUGUI _messageText;

        [SerializeField]
        private Button _confirmButton;

        private TextMeshProUGUI _buttonText;

        public event Action OnConfirmClicked;

        public void SetMessage(string message)
        {
            _messageText.text = message;
        }

        public void SetButtonText(string text)
        {
            _buttonText.text = text;
        }

        public void SetVisible(bool visible)
        {
            _panel.SetActive(visible);
        }

        public void Init(
            GameObject panel,
            TextMeshProUGUI messageText,
            Button confirmButton,
            TextMeshProUGUI buttonText)
        {
            _panel = panel;
            _messageText = messageText;
            _confirmButton = confirmButton;
            _buttonText = buttonText;
            _confirmButton.onClick.AddListener(() => OnConfirmClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _confirmButton.onClick.RemoveAllListeners();
        }
    }
}
