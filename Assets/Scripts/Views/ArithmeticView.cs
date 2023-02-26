using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArithmeticView : MonoBehaviour, IArithmeticView
{
    private const string okText = "That's right!";
    private const string errorText = "Wrong!";

    [SerializeField]
    private Text expressionText;
    [SerializeField]
    private Text detailText;
    [SerializeField]
    private Text correctAttemptText;
    [SerializeField]
    private Text incorrectAttemptText;
    [SerializeField]
    private Color errorColor = Color.red;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private SettingsPopupView settingsPopupPrefab;

    private void Start()
    {
        detailText.text = string.Empty;

        correctAttemptText.color = detailText.color;
        incorrectAttemptText.color = errorColor;

        settingsButton.onClick.AddListener(OnSettingsKeyClick);
    }

    private void OnDestroy()
    {
        settingsButton.onClick.RemoveListener(OnSettingsKeyClick);
    }

    #region IArithmeticView

    public void SetExpression(string value)
    {
        expressionText.text = value;
    }

    public void SetResultDetail(bool isRight, int correctAttempt, int incorrectAttempt)
    {
        SetResultDetailWithoutAnimation(correctAttempt, incorrectAttempt);

        StartCoroutine(ShowDetail(isRight));
    }

    public void SetResultDetailWithoutAnimation(int correctAttempt, int incorrectAttempt)
    {
        correctAttemptText.text = $"Correct: {correctAttempt}";
        incorrectAttemptText.text = $"Incorrect: {incorrectAttempt}";
    }

    public event EventHandler AfterResultDetail;

    public event EventHandler<ISettingsPopupView> SettingsKeyClick;

    #endregion

    private IEnumerator ShowDetail(bool isRight)
    {
        Color prevColor = detailText.color;
        detailText.text = isRight ? okText : errorText;

        if (!isRight)
        {
            detailText.color = errorColor;
        }

        yield return new WaitForSeconds(2);

        detailText.text = string.Empty;
        detailText.color = prevColor;

        AfterResultDetail?.Invoke(this, EventArgs.Empty);
    }

    private void OnSettingsKeyClick()
    {
        SettingsPopupView popup = Instantiate(settingsPopupPrefab);
        popup.transform.SetParent(transform.parent.parent, false);
        SettingsKeyClick?.Invoke(this, popup);
    }
}
