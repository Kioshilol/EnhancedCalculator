using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MessageBox
{
    public class MessageBoxView : MonoBehaviour, IMessageBoxView
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _confirmButton;

        public event Action OnConfirmClicked;

        public void SetMessage(string message)
        {
            _messageText.text = message;
        }

        public void SetVisible(bool visible)
        {
            _panel.SetActive(visible);
        }

        public void Init(
            GameObject panel,
            TextMeshProUGUI messageText,
            Button confirmButton)
        {
            _panel = panel;
            _messageText = messageText;
            _confirmButton = confirmButton;
            _confirmButton.onClick.AddListener(() => OnConfirmClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _confirmButton.onClick.RemoveAllListeners();
        }
    }
}
