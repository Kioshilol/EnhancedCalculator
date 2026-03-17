using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Calculator.Presentation;

namespace Calculator.View
{
    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        private TMP_InputField _inputField;
        private Button _resultButton;
        private Transform _historyContent;
        private GameObject _historyEntryPrefab;

        public event Action OnResultClicked;

        public string InputText
        {
            get => _inputField.text;
            set => _inputField.text = value;
        }

        public void Init(
            TMP_InputField inputField,
            Button resultButton,
            Transform historyContent,
            GameObject historyEntryPrefab)
        {
            _inputField = inputField;
            _resultButton = resultButton;
            _historyContent = historyContent;
            _historyEntryPrefab = historyEntryPrefab;

            _resultButton.onClick.AddListener(() => OnResultClicked?.Invoke());
        }

        public void SetHistory(List<string> history)
        {
            foreach (var child in _historyContent)
            {
                Destroy(((Transform)child).gameObject);
            }

            foreach (var entry in history)
            {
                CreateHistoryEntry(entry);
            }
        }

        public void AddHistoryEntry(string entry)
        {
            var go = Instantiate(_historyEntryPrefab, _historyContent);
            go.SetActive(true);
            go.transform.SetAsFirstSibling();
            go.GetComponent<TextMeshProUGUI>().text = entry;
        }

        private void CreateHistoryEntry(string entry)
        {
            var go = Instantiate(_historyEntryPrefab, _historyContent);
            go.SetActive(true);
            go.GetComponent<TextMeshProUGUI>().text = entry;
        }

        private void OnDestroy()
        {
            _resultButton.onClick.RemoveAllListeners();
        }
    }
}
