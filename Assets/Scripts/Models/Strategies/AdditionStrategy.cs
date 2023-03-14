public class AdditionStrategy : ArithmeticStrategyBase
{
    public override string GetExpression()
    {
        return $"{TermOne}+{TermTwo}=?";
    }

    public override bool GetResult(int value)
    {
        return value == TermOne + TermTwo;
    }
}