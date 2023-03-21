using System;

public interface ISettingsPopupView
{
    bool Addition { get; set; }
    bool Subtraction { get; set; }
    bool ResetProgress { get; }

    event EventHandler OnYesClick;
}