public class AdditionStrategy : AdditionSubtractionStrategyBase
{
    public AdditionStrategy(IArithmeticValue[] arithmeticValues, IRandomService randomService,
        IStoreService storeService) 
        : base(arithmeticValues, randomService, storeService) { }

    public override string GetExpression()
    {
        return $"{TermOne}+{TermTwo}=?";
    }

    public override bool GetResult(int value)
    {
        return value == TermOne + TermTwo;
    }

    public override ArithmeticTypes ArithmeticType => ArithmeticTypes.Addition;
}