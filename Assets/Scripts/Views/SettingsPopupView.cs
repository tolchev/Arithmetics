using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsPopupView : MonoBehaviour, ISettingsPopupView, IPointerClickHandler
{
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
    }

    private void OnDestroy()
    {
        yesButton.onClick.RemoveListener(YesButtonClick);
        noButton.onClick.RemoveListener(NoButtonClick);
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
}