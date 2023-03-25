using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsPopupView : MonoBehaviour, ISettingsPopupView, IPointerClickHandler
{
    [SerializeField]
    private Toggle addition;
    [SerializeField]
    private Toggle subtraction;
    [SerializeField]
    private Toggle resetProgress;
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    private void Start()
    {
        yesButton.onClick.AddListener(YesButtonClick);
        noButton.onClick.AddListener(NoButtonClick);
        addition.onValueChanged.AddListener(AdditionChanged);
        subtraction.onValueChanged.AddListener(SubtractionChanged);
    }

    private void OnDestroy()
    {
        yesButton.onClick.RemoveListener(YesButtonClick);
        noButton.onClick.RemoveListener(NoButtonClick);
        addition.onValueChanged.RemoveListener(AdditionChanged);
        subtraction.onValueChanged.RemoveListener(SubtractionChanged);
    }

    #region IPointerClickHandler

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        { 
            Destroy(gameObject);
        }
    }

    #endregion

    #region ISettingsPopupView

    public bool Addition
    {
        get => addition.isOn;
        set => addition.isOn = value;
    }

    public bool Subtraction
    {
        get => subtraction.isOn;
        set => subtraction.isOn = value;
    }

    public bool ResetProgress => resetProgress.isOn;

    public event EventHandler OnYesClick;

    #endregion

    private void YesButtonClick()
    {
        OnYesClick?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    private void NoButtonClick()
    {
        Destroy(gameObject);
    }

    private void AdditionChanged(bool value)
    {
        if (!value && !subtraction.isOn)
        {
            subtraction.isOn = true;
        }
    }

    private void SubtractionChanged(bool value)
    {
        if (!value && !addition.isOn)
        {
            addition.isOn = true;
        }
    }
}