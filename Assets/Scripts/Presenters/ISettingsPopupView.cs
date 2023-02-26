using System;

public interface ISettingsPopupView
{
    bool ResetProgress { get; }
    event EventHandler OnYesClick;
}