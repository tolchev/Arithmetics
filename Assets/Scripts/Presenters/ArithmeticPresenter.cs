using System;

public class ArithmeticPresenter : IDisposable
{
    private readonly IArithmeticView view;
    private readonly ArithmeticModel model;
    private SettingsPopupPresenter presenter = null;

    public ArithmeticPresenter(IArithmeticView view, ArithmeticModel model)
    {
        this.view = view;
        this.model = model;

        view.AfterResultDetail += AfterResultDetail;
        Messenger<int>.AddListener(MessageEventNames.CheckResultEvent, CheckResult);

        model.Start();
        view.SetExpression(model.Expression);
        view.SetResultDetailWithoutAnimation(model.CorrectAttempt, model.IncorrectAttempt);

        view.SettingsKeyClick += SettingsKeyClick;
    }

    private void SettingsKeyClick(object sender, ISettingsPopupView popupView)
    {
        void RefreshUI()
        {
            view.SetResultDetailWithoutAnimation(model.CorrectAttempt, model.IncorrectAttempt);
        }

        presenter = new SettingsPopupPresenter(popupView, model, RefreshUI);
    }

    public void Dispose()
    {
        Messenger<int>.RemoveListener(MessageEventNames.CheckResultEvent, CheckResult);
        view.AfterResultDetail -= AfterResultDetail;
        presenter?.Dispose();
    }

    private void CheckResult(int result)
    {
        bool isRight = model.CheckResultAndNext(result);
        view.SetResultDetail(isRight, model.CorrectAttempt, model.IncorrectAttempt, model.ShowPrize);
    }

    private void AfterResultDetail(object sender, EventArgs e)
    {
        view.SetExpression(model.Expression);
        Messenger.Broadcast(MessageEventNames.ResetEvent);
    }
}
