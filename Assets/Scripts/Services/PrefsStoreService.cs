using UnityEngine;

class PrefsStoreService : IStoreService
{
    private const string allAttemptKey = "AllAttempt";
    private const string correctAttemptKey = "CorrectAttempt";

    public int AllAttempt => PlayerPrefs.GetInt(allAttemptKey, 0);
    public int CorrectAttempt => PlayerPrefs.GetInt(correctAttemptKey, 0);

    public void Save(int allAttempt, int correctAttempt)
    {
        PlayerPrefs.SetInt(allAttemptKey, allAttempt);
        PlayerPrefs.SetInt(correctAttemptKey, correctAttempt);
    }
}
