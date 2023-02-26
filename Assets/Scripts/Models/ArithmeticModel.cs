using UnityEngine;

public class ArithmeticModel
{
    private readonly int minGeneratedResult;
    private readonly int maxGeneratedResult;
    private readonly int minTermValue;
    private readonly int maxTermValue;
    private readonly IStoreService storeService;

    private int termOne;
    private int termTwo;

    public ArithmeticModel(int minGeneratedResult, int maxGeneratedResult, int minTermValue, int maxTermValue, IStoreService storeService)
    {
        this.minGeneratedResult = minGeneratedResult;
        this.maxGeneratedResult = maxGeneratedResult;
        this.minTermValue = minTermValue;
        this.maxTermValue = maxTermValue;
        this.storeService = storeService ?? throw new System.ArgumentNullException(nameof(storeService));

        AllAttempt = storeService.AllAttempt;
        CorrectAttempt = storeService.CorrectAttempt;
    }

    public string Expression => $"{termOne}+{termTwo}=?";

    public void Start()
    {
        bool created = false;

        while (!created)
        {
            termOne = GenerateTerm();
            termTwo = GenerateTerm();

            int result = termOne + termTwo;
            if (result >= minGeneratedResult && result <= maxGeneratedResult)
            {
                created = true;
            }
        }
    }

    public bool CheckResultAndNext(int value)
    {
        var result = value == termOne + termTwo;

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
        return Random.Range(minTermValue, maxTermValue + 1);
    }

    private void SaveToStore()
    {
        storeService.Save(AllAttempt, CorrectAttempt);
    }
}