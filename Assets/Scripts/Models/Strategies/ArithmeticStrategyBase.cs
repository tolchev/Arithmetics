public abstract class ArithmeticStrategyBase : IArithmeticStrategy
{
    protected int TermOne { get; private set; }
    protected int TermTwo { get; private set; }

    public void SetTerms(int termOne, int termTwo)
    {
        TermOne = termOne;
        TermTwo = termTwo;
    }

    public abstract string GetExpression();
    public abstract bool GetResult(int value);
    public abstract ArithmeticTypes ArithmeticType { get; }
}
