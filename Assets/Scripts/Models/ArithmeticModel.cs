using System.Linq;

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
        Operations = storeService.Operations;
    }

    public string Expression => currentStrategy.GetExpression();

    public void Start()
    {
        bool created = false;

        int curIterationCount = 0;

        while (!created)
        {
            var filteredStrategies = strategies.Where(s => Operations.HasFlag(s.ArithmeticType)).ToArray();
            currentStrategy = filteredStrategies[randomService.Range(0, filteredStrategies.Length)];

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

        SaveAttemptsToStore();

        Start();
        return result;
    }

    public void ResetAttempts()
    {
        AllAttempt = 0;
        CorrectAttempt = 0;
        SaveAttemptsToStore();
    }

    public void SaveOperations(ArithmeticTypes operations)
    {
        if (operations == ArithmeticTypes.Unknown)
        {
            operations = ArithmeticTypes.Addition | ArithmeticTypes.Subtraction;
        }

        Operations = operations;
        storeService.SaveOperations(operations);
    }

    public int AllAttempt { get; private set; }
    public int CorrectAttempt { get; private set; }
    public int IncorrectAttempt => AllAttempt - CorrectAttempt;
    public ArithmeticTypes Operations { get; private set; }

    private int GenerateTerm()
    {
        return randomService.Range(minTermValue, maxTermValue + 1);
    }

    private void SaveAttemptsToStore()
    {
        storeService.SaveAttempts(AllAttempt, CorrectAttempt);
    }
}