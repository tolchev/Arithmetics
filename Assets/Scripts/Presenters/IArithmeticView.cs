﻿using System;

public interface IArithmeticView
{
    void SetExpression(string value);
    void SetResultDetail(bool isRight, int correctAttempt, int incorrectAttempt, bool showPrize);
    void SetResultDetailWithoutAnimation(int correctAttempt, int incorrectAttempt);
    event EventHandler AfterResultDetail;
    event EventHandler<ISettingsPopupView> SettingsKeyClick;
}