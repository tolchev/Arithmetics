using System;

public interface IKeyboardView
{
    event EventHandler<int> DigitKeyClick;
    event EventHandler BackspaceKeyClick;
    event EventHandler EnterKeyClick;
    void SetResult(string result);
    bool Interactable { get; set; }
}