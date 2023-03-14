public class ArithmeticModel
{
    private readonly int minGeneratedResult;
    private readonly int maxGeneratedResult;
    private readonly int minTermValue;
    private readonly int maxTermValue;
    private readonly IArithmeticStrategy[] strategies;
    private readonly IRandomService randomService;
    private readonly IStoreService storeService;

    private IArithmeticStrategy currentStrategy = null;

    private const int maxIterationCount = 30;

    public ArithmeticModel(int minGeneratedResult, int maxGeneratedResult, int minTermValue, int maxTermValue, 
        IArithmeticStrategy[] strategies, IRandomService randomService, IStoreService storeService)
    {
        this.minGeneratedResult = minGeneratedResult;
        this.maxGeneratedResult = maxGeneratedResult;
        this.minTermValue = minTermValue;
        this.maxTermValue = maxTermValue;
        this.strategies = strategies;
        this.randomService = randomService ?? throw new System.ArgumentNullException(nameof(randomService));
        this.storeService = storeService ?? throw new System.ArgumentNullException(nameof(storeService));

        AllAttempt = storeService.AllAttempt;
        CorrectAttempt = storeService.CorrectAttempt;
    }

    public string Expression => currentStrategy.GetExpression();

    public void Start()
    {
        bool created = false;

        int curIterationCount = 0;

        while (!created)
        {
            currentStrategy = strategies[randomService.Range(0, strategies.Length)];

            int termOne = GenerateTerm();
            int termTwo = GenerateTerm();

            int result = termOne + termTwo;
            if (result >= minGeneratedResult && result <= maxGeneratedResult || curIterationCount++ >= maxIterationCount)
            {
                created = true;
                currentStrategy.SetTerms(termOne, termTwo);
            }
        }
    }

    public bool CheckResultAndNext(int value)
    {
        var result = currentStrategy.GetResult(value);

        AllAttempt++;
        if (result)
        {
            CorrectAttempt++;
        }

        SaveToStore();

        Start();
        return result;
    }

    public void ResetAttempts()
    {
        AllAttempt = 0;
        CorrectAttempt = 0;
        SaveToStore();
    }

    public int AllAttempt { get; private set; }
    public int CorrectAttempt { get; private set; }
    public int IncorrectAttempt => AllAttempt - CorrectAttempt;

    private int GenerateTerm()
    {
        return randomService.Range(minTermValue, maxTermValue + 1);
    }

    private void SaveToStore()
    {
        storeService.Save(AllAttempt, CorrectAttempt);
    }
}