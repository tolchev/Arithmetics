using System;

public class KeyboardPresenter : IDisposable
{
    private readonly IKeyboardView view;
    private readonly KeyboardModel model;

    public KeyboardPresenter(IKeyboardView view, KeyboardModel model)
    {
        this.view = view;
        this.model = model;

        view.DigitKeyClick += DigitKeyClick;
        view.BackspaceKeyClick += BackspaceKeyClick;
        view.EnterKeyClick += EnterKeyClick;

        UpdateResult();

        Messenger.AddListener(MessageEventNames.ResetEvent, Reset);
    }

    public void Dispose()
    {
        view.DigitKeyClick -= DigitKeyClick;
        view.BackspaceKeyClick -= BackspaceKeyClick;
        view.EnterKeyClick -= EnterKeyClick;
    }

    private void DigitKeyClick(object sender, int e)
    {
        try
        {
            model.DigitPress(e);
        }
        catch (OverflowException) { }
        UpdateResult();
    }

    private void BackspaceKeyClick(object sender, EventArgs e)
    {
        model.BackspacePress();
        UpdateResult();
    }

    private void EnterKeyClick(object sender, EventArgs e)
    {
        if (model.ResultText.Length > 0)
        {
            view.Interactable = false;
            Messenger<int>.Broadcast(MessageEventNames.CheckResultEvent, model.Result);
        }
    }

    private void UpdateResult()
    {
        view.SetResult(model.ResultText);
    }

    private void Reset()
    {
        model.Reset();
        UpdateResult();
        view.Interactable = true;
    }
}
