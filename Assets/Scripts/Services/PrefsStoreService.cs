using UnityEngine;

class PrefsStoreService : IStoreService
{
    private const string allAttemptKey = nameof(AllAttempt);
    private const string correctAttemptKey = nameof(CorrectAttempt);
    private const string operationsKey = nameof(Operations);

    public int AllAttempt => PlayerPrefs.GetInt(allAttemptKey, 0);
    public int CorrectAttempt => PlayerPrefs.GetInt(correctAttemptKey, 0);

    public void SaveAttempts(int allAttempt, int correctAttempt)
    {
        PlayerPrefs.SetInt(allAttemptKey, allAttempt);
        PlayerPrefs.SetInt(correctAttemptKey, correctAttempt);
        PlayerPrefs.Save();
    }

    public ArithmeticTypes Operations => (ArithmeticTypes)PlayerPrefs.GetInt(operationsKey, (int)(ArithmeticTypes.Addition | ArithmeticTypes.Subtraction));

    public void SaveOperations(ArithmeticTypes operations)
    {
        PlayerPrefs.SetInt(operationsKey, (int)operations);
        PlayerPrefs.Save();
    }
}
