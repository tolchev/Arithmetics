using System.Collections.Generic;
using System.Linq;

public abstract class AdditionSubtractionStrategyBase : ArithmeticStrategyBase
{
    private readonly IDictionary<ArithmeticValueType, IArithmeticValue> arithmeticValues;
    private readonly IRandomService randomService;
    private readonly IStoreService storeService;

    private const int maxIterationCount = 30;

    public AdditionSubtractionStrategyBase(IArithmeticValue[] arithmeticValues, IRandomService randomService, 
        IStoreService storeService)
    {
        this.arithmeticValues = arithmeticValues.ToDictionary(a => a.ArithmeticValueType);
        this.randomService = randomService ?? throw new System.ArgumentNullException(nameof(randomService));
        this.storeService = storeService ?? throw new System.ArgumentNullException(nameof(storeService));
    }

    public override void GenerateTerms(out int termOne, out int termTwo)
    {
        termOne = 0;
        termTwo = 0;

        bool created = false;

        int curIterationCount = 0;

        while (!created)
        {
            termOne = GenerateTerm();
            termTwo = GenerateTerm();

            int result = termOne + termTwo;

            if (result >= CurrentArithmeticValue.MinGeneratedResult && result <= CurrentArithmeticValue.MaxGeneratedResult
                || curIterationCount++ >= maxIterationCount)
            {
                created = true;
            }
        }
    }

    private IArithmeticValue CurrentArithmeticValue => arithmeticValues[storeService.ValueType];

    private int GenerateTerm()
    {
        return randomService.Range(CurrentArithmeticValue.MinTermValue, CurrentArithmeticValue.MaxTermValue + 1);
    }
}