using System.Collections.Generic;
using System.Linq;

public class ArithmeticModel
{
    private readonly IArithmeticStrategy[] strategies;
    private readonly IRandomService randomService;
    private readonly IStoreService storeService;

    private IArithmeticStrategy currentStrategy = null;

    public ArithmeticModel(IArithmeticStrategy[] strategies, 
        IRandomService randomService, IStoreService storeService)
    {
        this.strategies = strategies;
        this.randomService = randomService ?? throw new System.ArgumentNullException(nameof(randomService));
        this.storeService = storeService ?? throw new System.ArgumentNullException(nameof(storeService));

        AllAttempt = storeService.AllAttempt;
        CorrectAttempt = storeService.CorrectAttempt;
        Operations = storeService.Operations;
        ValueType = storeService.ValueType;
    }

    public string Expression => currentStrategy.GetExpression();

    public void Start()
    {
        var filteredStrategies = strategies.Where(s => Operations.HasFlag(s.ArithmeticType)).ToArray();
        currentStrategy = filteredStrategies[randomService.Range(0, filteredStrategies.Length)];

        currentStrategy.GenerateTerms(out int termOne, out int termTwo);
        currentStrategy.SetTerms(termOne, termTwo);
    }

    public bool CheckResultAndNext(int value)
    {
        var result = currentStrategy.GetResult(value);

        AllAttempt++;
        if (result)
        {
            CorrectAttempt++;
            ShowPrize = CorrectAttempt % 10 == 0;
        }
        else
        {
            ShowPrize = false;
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

    public void SaveProperties(ArithmeticTypes operations, ArithmeticValueType valueType)
    {
        if (operations == ArithmeticTypes.Unknown)
        {
            operations = ArithmeticTypes.Addition | ArithmeticTypes.Subtraction;
        }

        Operations = operations;
        ValueType = valueType;
        storeService.SaveProperties(operations, valueType);
    }

    public int AllAttempt { get; private set; }
    public int CorrectAttempt { get; private set; }
    public int IncorrectAttempt => AllAttempt - CorrectAttempt;
    public bool ShowPrize { get; private set; }
    public ArithmeticTypes Operations { get; private set; }
    public ArithmeticValueType ValueType { get; private set; }

    private void SaveAttemptsToStore()
    {
        storeService.SaveAttempts(AllAttempt, CorrectAttempt);
    }
}