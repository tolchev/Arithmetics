using UnityEngine;

public class ArithmeticModel
{
    private readonly int minGeneratedResult;
    private readonly int maxGeneratedResult;
    private readonly int minTermValue;
    private readonly int maxTermValue;

    private int termOne;
    private int termTwo;

    public ArithmeticModel(int minGeneratedResult, int maxGeneratedResult, int minTermValue, int maxTermValue)
    {
        this.minGeneratedResult = minGeneratedResult;
        this.maxGeneratedResult = maxGeneratedResult;
        this.minTermValue = minTermValue;
        this.maxTermValue = maxTermValue;
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

        Start();
        return result;
    }

    public int AllAttempt { get; private set; }
    public int CorrectAttempt { get; private set; }
    public int IncorrectAttempt => AllAttempt - CorrectAttempt;

    private int GenerateTerm()
    {
        return Random.Range(minTermValue, maxTermValue + 1);
    }
}