public class SubtractionStrategy : ArithmeticStrategyBase
{
    public override string GetExpression()
    {
        return $"{TermOne + TermTwo}-{TermTwo}=?";
    }

    public override bool GetResult(int value)
    {
        return value == TermOne;
    }

    public override ArithmeticTypes ArithmeticType => ArithmeticTypes.Subtraction;
}
