using System;
using System.Linq;
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
    private Toggle multiplication;
    [SerializeField]
    private Toggle moreDifficult;
    [SerializeField]
    private Toggle resetProgress;
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    private Toggle[] operationToggles;

    private void Start()
    {
        yesButton.onClick.AddListener(YesButtonClick);
        noButton.onClick.AddListener(NoButtonClick);
        addition.onValueChanged.AddListener(OperationTogglesChanged);
        subtraction.onValueChanged.AddListener(OperationTogglesChanged);
        multiplication.onValueChanged.AddListener(OperationTogglesChanged);

        operationToggles = new Toggle[] { addition, subtraction, multiplication };
    }

    private void OnDestroy()
    {
        yesButton.onClick.RemoveListener(YesButtonClick);
        noButton.onClick.RemoveListener(NoButtonClick);
        addition.onValueChanged.RemoveListener(OperationTogglesChanged);
        subtraction.onValueChanged.RemoveListener(OperationTogglesChanged);
        multiplication.onValueChanged.RemoveListener(OperationTogglesChanged);
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

    public bool Multiplication
    {
        get => multiplication.isOn;
        set => multiplication.isOn = value;
    }

    public bool MoreDifficult
    {
        get => moreDifficult.isOn;
        set => moreDifficult.isOn = value;
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

    private void OperationTogglesChanged(bool _)
    {
        if (operationToggles.All(t => !t.isOn))
        {
            addition.isOn = true;
        }
    }
}