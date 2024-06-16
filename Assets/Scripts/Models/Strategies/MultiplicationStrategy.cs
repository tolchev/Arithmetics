public class MultiplicationStrategy : ArithmeticStrategyBase
{
    private readonly IRandomService randomService;

    public MultiplicationStrategy(IRandomService randomService)
    {
        this.randomService = randomService ?? throw new System.ArgumentNullException(nameof(randomService));
    }

    public override void GenerateTerms(out int termOne, out int termTwo)
    {
        termOne = GenerateTerm();
        termTwo = GenerateTerm();
    }

    public override string GetExpression()
    {
        return $"{TermOne}*{TermTwo}=?";
    }

    public override bool GetResult(int value)
    {
        return value == TermOne * TermTwo;
    }

    public override ArithmeticTypes ArithmeticType => ArithmeticTypes.Multiplication;

    private int GenerateTerm()
    {
        return randomService.Range(1, 10);
    }
}
