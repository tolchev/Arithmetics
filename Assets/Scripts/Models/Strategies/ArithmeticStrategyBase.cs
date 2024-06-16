using System;

public abstract class ArithmeticStrategyBase : IArithmeticStrategy
{
    protected int TermOne { get; private set; }
    protected int TermTwo { get; private set; }

    [Obsolete("Убрать и использовать GenerateTerms")]
    public void SetTerms(int termOne, int termTwo)
    {
        TermOne = termOne;
        TermTwo = termTwo;
    }

    public abstract void GenerateTerms(out int termOne, out int termTwo);
    public abstract string GetExpression();
    public abstract bool GetResult(int value);
    public abstract ArithmeticTypes ArithmeticType { get; }
}
