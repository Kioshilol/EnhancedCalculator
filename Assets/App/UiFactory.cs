using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Calculator.View;
using MessageBox;

public class UiFactory
{
    private MessageBoxView _messageBoxView;

    public MessageBoxView MessageBoxView => _messageBoxView;
    
    public Canvas Canvas { get; private set; }

    public CalculatorView CalculatorView { get; private set; }

    public TMP_InputField InputField { get; private set; }

    public GameObject MessageBoxPanel { get; private set; }

    public void Build()
    {
        Canvas = CreateCanvas();
        MessageBoxPanel = BuildMessageBox(Canvas.transform, out _messageBoxView);
        MessageBoxPanel.SetActive(false);
        
        BuildCalculator(Canvas.transform);
    }

    private static Canvas CreateCanvas()
    {
        var canvasGo = new GameObject("Canvas");
        var canvas = canvasGo.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var scaler = canvasGo.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        scaler.matchWidthOrHeight = 0.5f;

        canvasGo.AddComponent<GraphicRaycaster>();
        return canvas;
    }

    private void BuildCalculator(Transform parent)
    {
        var bg = CreatePanel(
            parent, 
            "Background", 
            Color.black,
            Vector2.zero,
            Vector2.one);

        var cardRt = CreatePanel(
            bg.transform, 
            "Card",
            Color.white,
            Vector2.zero, 
            Vector2.zero)
            .GetComponent<RectTransform>();
        cardRt.anchorMin = new Vector2(0.5f, 0.5f);
        cardRt.anchorMax = new Vector2(0.5f, 0.5f);
        cardRt.sizeDelta = new Vector2(700, 0);
        cardRt.anchoredPosition = Vector2.zero;

        var csf = cardRt.gameObject.AddComponent<ContentSizeFitter>();
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        var vlg = cardRt.gameObject.AddComponent<VerticalLayoutGroup>();
        vlg.padding = new RectOffset(30, 30, 30, 30);
        vlg.spacing = 15;
        vlg.childAlignment = TextAnchor.UpperCenter;
        vlg.childControlWidth = true;
        vlg.childControlHeight = true;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;

        CreateTitle(cardRt.transform);
        InputField = CreateInputField(cardRt.transform);
        var historyContent = CreateHistoryArea(cardRt.transform);
        var historyEntryPrefab = CreateHistoryEntryPrefab();
        var resultButton = CreateResultButton(cardRt.transform);

        CalculatorView = cardRt.gameObject.AddComponent<CalculatorView>();
        CalculatorView.Init(InputField, resultButton, historyContent, historyEntryPrefab);
    }

    private static void CreateTitle(Transform parent)
    {
        var titleGo = new GameObject("Title");
        titleGo.transform.SetParent(parent, false);

        var titleText = titleGo.AddComponent<TextMeshProUGUI>();
        titleText.text = "CALCULATOR PRO \u00AE";
        titleText.fontSize = 36;
        titleText.fontStyle = FontStyles.Bold;
        titleText.alignment = TextAlignmentOptions.Center;
        titleText.color = Color.black;

        var le = titleGo.AddComponent<LayoutElement>();
        le.preferredHeight = 50;
    }

    private TMP_InputField CreateInputField(Transform parent)
    {
        var inputGo = new GameObject("InputField");
        inputGo.SetActive(false);
        inputGo.transform.SetParent(parent, false);

        inputGo.AddComponent<RectTransform>();
        var inputLe = inputGo.AddComponent<LayoutElement>();
        inputLe.preferredHeight = 70;

        var inputImage = inputGo.AddComponent<Image>();
        inputImage.color = Color.white;

        var textAreaGo = new GameObject("Text Area");
        textAreaGo.transform.SetParent(inputGo.transform, false);
        var textAreaRT = textAreaGo.AddComponent<RectTransform>();
        textAreaRT.anchorMin = Vector2.zero;
        textAreaRT.anchorMax = Vector2.one;
        textAreaRT.offsetMin = new Vector2(10, 0);
        textAreaRT.offsetMax = new Vector2(-10, 0);
        textAreaGo.AddComponent<RectMask2D>();

        var placeholderGo = new GameObject("Placeholder");
        placeholderGo.transform.SetParent(textAreaGo.transform, false);
        var placeholderRT = placeholderGo.AddComponent<RectTransform>();
        placeholderRT.anchorMin = Vector2.zero;
        placeholderRT.anchorMax = Vector2.one;
        placeholderRT.offsetMin = Vector2.zero;
        placeholderRT.offsetMax = Vector2.zero;
        var placeholderText = placeholderGo.AddComponent<TextMeshProUGUI>();
        placeholderText.text = "Enter an equation...";
        placeholderText.fontSize = 32;
        placeholderText.fontStyle = FontStyles.Italic;
        placeholderText.color = new Color(0.7f, 0.7f, 0.7f);
        placeholderText.alignment = TextAlignmentOptions.MidlineLeft;
        placeholderText.raycastTarget = false;

        var textGo = new GameObject("Text");
        textGo.transform.SetParent(textAreaGo.transform, false);
        var textRT = textGo.AddComponent<RectTransform>();
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;
        textRT.offsetMin = Vector2.zero;
        textRT.offsetMax = Vector2.zero;
        var inputText = textGo.AddComponent<TextMeshProUGUI>();
        inputText.fontSize = 32;
        inputText.color = Color.black;
        inputText.alignment = TextAlignmentOptions.MidlineLeft;
        inputText.raycastTarget = false;

        var inputField = inputGo.AddComponent<TMP_InputField>();
        inputField.textViewport = textAreaRT;
        inputField.textComponent = inputText;
        inputField.placeholder = placeholderText;
        inputField.fontAsset = inputText.font;

        inputGo.SetActive(true);

        var underline = new GameObject("Underline");
        underline.transform.SetParent(inputGo.transform, false);
        var underlineRT = underline.AddComponent<RectTransform>();
        underlineRT.anchorMin = new Vector2(0, 0);
        underlineRT.anchorMax = new Vector2(1, 0);
        underlineRT.sizeDelta = new Vector2(0, 3);
        underlineRT.anchoredPosition = Vector2.zero;
        var underlineImg = underline.AddComponent<Image>();
        underlineImg.color = new Color(0.53f, 0.81f, 0.92f);

        inputImage.color = new Color(1, 1, 1, 0);

        return inputField;
    }

    private Transform CreateHistoryArea(Transform parent)
    {
        var scrollGo = new GameObject("HistoryScroll");
        scrollGo.transform.SetParent(parent, false);

        var scrollLe = scrollGo.AddComponent<LayoutElement>();
        scrollLe.flexibleHeight = 1;
        scrollLe.preferredHeight = 500;

        var scrollImage = scrollGo.AddComponent<Image>();
        scrollImage.color = new Color(1, 1, 1, 0);

        var scrollRect = scrollGo.AddComponent<ScrollRect>();
        scrollRect.horizontal = false;
        scrollRect.vertical = true;

        scrollGo.AddComponent<RectMask2D>();

        var viewportGo = new GameObject("Viewport");
        viewportGo.transform.SetParent(scrollGo.transform, false);
        var viewportRT = viewportGo.AddComponent<RectTransform>();
        viewportRT.anchorMin = Vector2.zero;
        viewportRT.anchorMax = Vector2.one;
        viewportRT.offsetMin = Vector2.zero;
        viewportRT.offsetMax = new Vector2(-15, 0);

        var contentGo = new GameObject("Content");
        contentGo.transform.SetParent(viewportGo.transform, false);
        var contentRT = contentGo.AddComponent<RectTransform>();
        contentRT.anchorMin = new Vector2(0, 1);
        contentRT.anchorMax = new Vector2(1, 1);
        contentRT.pivot = new Vector2(0.5f, 1);
        contentRT.offsetMin = new Vector2(0, 0);
        contentRT.offsetMax = new Vector2(0, 0);

        var csf = contentGo.AddComponent<ContentSizeFitter>();
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        var contentVlg = contentGo.AddComponent<VerticalLayoutGroup>();
        contentVlg.childAlignment = TextAnchor.UpperRight;
        contentVlg.childControlWidth = true;
        contentVlg.childControlHeight = true;
        contentVlg.childForceExpandWidth = true;
        contentVlg.childForceExpandHeight = false;
        contentVlg.spacing = 5;

        scrollRect.content = contentRT;
        scrollRect.viewport = viewportRT;

        var scrollbar = CreateVerticalScrollbar(scrollGo.transform);
        scrollRect.verticalScrollbar = scrollbar;
        scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;

        return contentRT;
    }

    private static Scrollbar CreateVerticalScrollbar(Transform parent)
    {
        var scrollbarGo = new GameObject("Scrollbar");
        scrollbarGo.transform.SetParent(parent, false);
        var scrollbarRT = scrollbarGo.AddComponent<RectTransform>();
        scrollbarRT.anchorMin = new Vector2(1, 0);
        scrollbarRT.anchorMax = new Vector2(1, 1);
        scrollbarRT.pivot = new Vector2(1, 0.5f);
        scrollbarRT.sizeDelta = new Vector2(8, 0);

        var scrollbarImage = scrollbarGo.AddComponent<Image>();
        scrollbarImage.color = new Color(0.8f, 0.8f, 0.8f, 0.5f);

        var handleArea = new GameObject("Handle Slide Area");
        handleArea.transform.SetParent(scrollbarGo.transform, false);
        var handleAreaRT = handleArea.AddComponent<RectTransform>();
        handleAreaRT.anchorMin = Vector2.zero;
        handleAreaRT.anchorMax = Vector2.one;
        handleAreaRT.offsetMin = Vector2.zero;
        handleAreaRT.offsetMax = Vector2.zero;

        var handle = new GameObject("Handle");
        handle.transform.SetParent(handleArea.transform, false);
        var handleRT = handle.AddComponent<RectTransform>();
        handleRT.anchorMin = Vector2.zero;
        handleRT.anchorMax = Vector2.one;
        handleRT.offsetMin = Vector2.zero;
        handleRT.offsetMax = Vector2.zero;
        var handleImage = handle.AddComponent<Image>();
        handleImage.color = new Color(0.53f, 0.81f, 0.92f);

        var scrollbar = scrollbarGo.AddComponent<Scrollbar>();
        scrollbar.handleRect = handleRT;
        scrollbar.direction = Scrollbar.Direction.BottomToTop;
        scrollbar.targetGraphic = handleImage;

        return scrollbar;
    }

    private static GameObject CreateHistoryEntryPrefab()
    {
        var prefab = new GameObject("HistoryEntryPrefab");
        prefab.SetActive(false);

        var text = prefab.AddComponent<TextMeshProUGUI>();
        text.fontSize = 30;
        text.fontStyle = FontStyles.Bold;
        text.alignment = TextAlignmentOptions.Right;
        text.color = new Color(0.53f, 0.81f, 0.92f);

        var le = prefab.AddComponent<LayoutElement>();
        le.preferredHeight = 40;

        Object.DontDestroyOnLoad(prefab);
        return prefab;
    }

    private static Button CreateResultButton(Transform parent)
    {
        var btnGo = new GameObject("ResultButton");
        btnGo.transform.SetParent(parent, false);

        var btnLe = btnGo.AddComponent<LayoutElement>();
        btnLe.preferredHeight = 70;

        var btnImage = btnGo.AddComponent<Image>();
        btnImage.color = new Color(0.53f, 0.81f, 0.92f);

        var btn = btnGo.AddComponent<Button>();
        btn.targetGraphic = btnImage;

        var colors = btn.colors;
        colors.normalColor = new Color(0.53f, 0.81f, 0.92f);
        colors.highlightedColor = new Color(0.43f, 0.71f, 0.82f);
        colors.pressedColor = new Color(0.33f, 0.61f, 0.72f);
        btn.colors = colors;

        var textGo = new GameObject("Text");
        textGo.transform.SetParent(btnGo.transform, false);
        var textRT = textGo.AddComponent<RectTransform>();
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;
        textRT.offsetMin = Vector2.zero;
        textRT.offsetMax = Vector2.zero;

        var btnText = textGo.AddComponent<TextMeshProUGUI>();
        btnText.text = "RESULT";
        btnText.fontSize = 36;
        btnText.fontStyle = FontStyles.Bold;
        btnText.alignment = TextAlignmentOptions.Center;
        btnText.color = Color.white;

        return btn;
    }

    private static GameObject BuildMessageBox(
        Transform parent,
        out MessageBoxView view)
    {
        var overlay = CreatePanel(
            parent, 
            "MessageBoxOverlay",
            new Color(0, 0, 0, 0.7f),
            Vector2.zero, 
            Vector2.one);
        overlay.transform.SetAsLastSibling();

        var cardRT = CreatePanel(
            overlay.transform,
            "MessageBoxCard", 
            Color.white, 
            Vector2.zero,
            Vector2.zero)
            .GetComponent<RectTransform>();
        cardRT.anchorMin = new Vector2(0.5f, 0.5f);
        cardRT.anchorMax = new Vector2(0.5f, 0.5f);
        cardRT.sizeDelta = new Vector2(600, 280);

        var vlg = cardRT.gameObject.AddComponent<VerticalLayoutGroup>();
        vlg.padding = new RectOffset(30, 30, 40, 30);
        vlg.spacing = 20;
        vlg.childAlignment = TextAnchor.MiddleCenter;
        vlg.childControlWidth = true;
        vlg.childControlHeight = false;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;

        var msgGo = new GameObject("Message");
        msgGo.transform.SetParent(cardRT, false);
        var msgText = msgGo.AddComponent<TextMeshProUGUI>();
        msgText.fontSize = 32;
        msgText.fontStyle = FontStyles.Bold;
        msgText.alignment = TextAlignmentOptions.Center;
        msgText.color = Color.black;
        var msgLe = msgGo.AddComponent<LayoutElement>();
        msgLe.preferredHeight = 100;

        var btnGo = new GameObject("GotItButton");
        btnGo.transform.SetParent(cardRT, false);
        var btnImage = btnGo.AddComponent<Image>();
        btnImage.color = new Color(0.53f, 0.81f, 0.92f);
        var btn = btnGo.AddComponent<Button>();
        btn.targetGraphic = btnImage;
        var btnLe = btnGo.AddComponent<LayoutElement>();
        btnLe.preferredHeight = 60;

        var btnTextGo = new GameObject("Text");
        btnTextGo.transform.SetParent(btnGo.transform, false);
        var btnTextRT = btnTextGo.AddComponent<RectTransform>();
        btnTextRT.anchorMin = Vector2.zero;
        btnTextRT.anchorMax = Vector2.one;
        btnTextRT.offsetMin = Vector2.zero;
        btnTextRT.offsetMax = Vector2.zero;
        var btnText = btnTextGo.AddComponent<TextMeshProUGUI>();
        btnText.text = "GOT IT";
        btnText.fontSize = 32;
        btnText.fontStyle = FontStyles.Bold;
        btnText.alignment = TextAlignmentOptions.Center;
        btnText.color = Color.white;

        view = overlay.AddComponent<MessageBoxView>();
        view.Init(overlay, msgText, btn);

        return overlay;
    }

    private static GameObject CreatePanel(
        Transform parent,
        string name,
        Color color,
        Vector2 anchorMin,
        Vector2 anchorMax)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);

        var rt = go.AddComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        var image = go.AddComponent<Image>();
        image.color = color;

        return go;
    }
}
