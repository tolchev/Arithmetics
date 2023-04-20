using System.Collections.Generic;
using System.Linq;

public class ArithmeticModel
{
    private readonly IDictionary<ArithmeticValueType, IArithmeticValue> arithmeticValues;
    private readonly IArithmeticStrategy[] strategies;
    private readonly IRandomService randomService;
    private readonly IStoreService storeService;

    private IArithmeticStrategy currentStrategy = null;

    private const int maxIterationCount = 30;

    public ArithmeticModel(IArithmeticValue[] arithmeticValues, IArithmeticStrategy[] strategies, 
        IRandomService randomService, IStoreService storeService)
    {
        this.arithmeticValues = arithmeticValues.ToDictionary(a => a.ArithmeticValueType);
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
        bool created = false;

        int curIterationCount = 0;

        while (!created)
        {
            var filteredStrategies = strategies.Where(s => Operations.HasFlag(s.ArithmeticType)).ToArray();
            currentStrategy = filteredStrategies[randomService.Range(0, filteredStrategies.Length)];

            int termOne = GenerateTerm();
            int termTwo = GenerateTerm();

            int result = termOne + termTwo;
            
            if (result >= CurrentArithmeticValue.MinGeneratedResult && result <= CurrentArithmeticValue.MaxGeneratedResult 
                || curIterationCount++ >= maxIterationCount)
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

    private IArithmeticValue CurrentArithmeticValue => arithmeticValues[ValueType];

    private int GenerateTerm()
    {
        return randomService.Range(CurrentArithmeticValue.MinTermValue, CurrentArithmeticValue.MaxTermValue + 1);
    }

    private void SaveAttemptsToStore()
    {
        storeService.SaveAttempts(AllAttempt, CorrectAttempt);
    }
}