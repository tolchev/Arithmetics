using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class KeyboardView : MonoBehaviour, IKeyboardView
{
    [SerializeField]
    private Button[] digitKeys;
    [SerializeField]
    private Button backspaceKey;
    [SerializeField]
    private Button enterKey;
    [SerializeField]
    private Text resultText;

    private CanvasGroup keyboardCanvasGroup;

    private void Start()
    {
        keyboardCanvasGroup = GetComponent<CanvasGroup>();

        for (int i = 0; i < digitKeys.Length; i++)
        {
            var key = digitKeys[i];
            var j = i;
            key.onClick.AddListener(() => OnDigitKeyClick(j));
        }

        backspaceKey.onClick.AddListener(OnBackspaceKeyClick);
        enterKey.onClick.AddListener(OnEnterKeyClick);
    }

    private void OnDestroy()
    {
        backspaceKey.onClick.RemoveListener(OnBackspaceKeyClick);
        enterKey.onClick.RemoveListener(OnEnterKeyClick);
    }

    #region IKeyboardView

    public event EventHandler<int> DigitKeyClick;
    public event EventHandler BackspaceKeyClick;
    public event EventHandler EnterKeyClick;

    public void SetResult(string result)
    {
        resultText.text = result;
    }

    public bool Interactable 
    {
        get => keyboardCanvasGroup.interactable;
        set => keyboardCanvasGroup.interactable = value;
    }

    #endregion

    private void OnDigitKeyClick(int key)
    {
        DigitKeyClick?.Invoke(this, key);
    }

    private void OnBackspaceKeyClick()
    {
        BackspaceKeyClick?.Invoke(this, EventArgs.Empty);
    }

    private void OnEnterKeyClick()
    {
        EnterKeyClick?.Invoke(this, EventArgs.Empty);
    }
}
