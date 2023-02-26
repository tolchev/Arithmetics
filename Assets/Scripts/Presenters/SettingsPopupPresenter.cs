using System;

public class SettingsPopupPresenter : IDisposable
{
    private readonly ISettingsPopupView view;
    private readonly ArithmeticModel model;
    private readonly Action onYesCallback;

    public SettingsPopupPresenter(ISettingsPopupView view, ArithmeticModel model, Action onYesCallback)
    {
        this.view = view ?? throw new ArgumentNullException(nameof(view));
        this.model = model ?? throw new ArgumentNullException(nameof(model));
        this.onYesCallback = onYesCallback ?? throw new ArgumentNullException(nameof(onYesCallback));

        view.OnYesClick += OnYesClick;
    }

    private void OnYesClick(object sender, EventArgs e)
    {
        if (view.ResetProgress)
        {
            model.ResetAttempts();
        }
        onYesCallback();
    }

    public void Dispose()
    {
        view.OnYesClick -= OnYesClick;
    }
}