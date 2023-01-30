using System;

public class ArithmeticPresenter : IDisposable
{
    IArithmeticView view;
    ArithmeticModel model;

    public ArithmeticPresenter(IArithmeticView view, ArithmeticModel model)
    {
        this.view = view;
        this.model = model;

        view.AfterResultDetail += AfterResultDetail;
        Messenger<int>.AddListener(MessageEventNames.CheckResultEvent, CheckResult);

        model.Start();
        view.SetExpression(model.Expression);
    }

    public void Dispose()
    {
        Messenger<int>.RemoveListener(MessageEventNames.CheckResultEvent, CheckResult);
        view.AfterResultDetail -= AfterResultDetail;
    }

    private void CheckResult(int result)
    {
        bool isRight = model.CheckResultAndNext(result);
        view.SetResultDetail(isRight, model.CorrectAttempt, model.IncorrectAttempt);
    }

    private void AfterResultDetail(object sender, EventArgs e)
    {
        view.SetExpression(model.Expression);
        Messenger.Broadcast(MessageEventNames.ResetEvent);
    }
}
