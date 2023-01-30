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

    private void Start()
    {
        detailText.text = string.Empty;

        correctAttemptText.color = detailText.color;
        incorrectAttemptText.color = errorColor;

        SetAttempts(0, 0);
    }

    #region IArithmeticView

    public void SetExpression(string value)
    {
        expressionText.text = value;
    }

    public void SetResultDetail(bool isRight, int correctAttempt, int incorrectAttempt)
    {
        SetAttempts(correctAttempt, incorrectAttempt);

        StartCoroutine(ShowDetail(isRight));
    }

    public event EventHandler AfterResultDetail;

    #endregion

    private void SetAttempts(int correctAttempt, int incorrectAttempt)
    {
        correctAttemptText.text = $"Correct: {correctAttempt}";
        incorrectAttemptText.text = $"Incorrect: {incorrectAttempt}";
    }

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
}
