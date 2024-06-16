using System;

public interface ISettingsPopupView
{
    bool Addition { get; set; }
    bool Subtraction { get; set; }
    bool Multiplication { get; set; }
    bool MoreDifficult { get; set; }
    bool ResetProgress { get; }

    event EventHandler OnYesClick;
}