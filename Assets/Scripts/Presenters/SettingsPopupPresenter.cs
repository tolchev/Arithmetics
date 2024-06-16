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
        view.Addition = model.Operations.HasFlag(ArithmeticTypes.Addition);
        view.Subtraction = model.Operations.HasFlag(ArithmeticTypes.Subtraction);
        view.Multiplication = model.Operations.HasFlag(ArithmeticTypes.Multiplication);
        view.MoreDifficult = model.ValueType == ArithmeticValueType.Hard;
    }

    private void OnYesClick(object sender, EventArgs e)
    {
        if (view.ResetProgress)
        {
            model.ResetAttempts();
        }

        ArithmeticTypes operations = (view.Addition ? ArithmeticTypes.Addition : ArithmeticTypes.Unknown)
            | (view.Subtraction ? ArithmeticTypes.Subtraction : ArithmeticTypes.Unknown)
            | (view.Multiplication ? ArithmeticTypes.Multiplication : ArithmeticTypes.Unknown);

        model.SaveProperties(operations, view.MoreDifficult ? ArithmeticValueType.Hard : ArithmeticValueType.Easy);
        onYesCallback();
    }

    public void Dispose()
    {
        view.OnYesClick -= OnYesClick;
    }
}