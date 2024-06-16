public class SubtractionStrategy : AdditionSubtractionStrategyBase
{
    public SubtractionStrategy(IArithmeticValue[] arithmeticValues, IRandomService randomService,
        IStoreService storeService)
        : base(arithmeticValues, randomService, storeService) { }

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
