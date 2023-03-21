public interface IStoreService
{
    int AllAttempt { get; }
    int CorrectAttempt { get; }
    void SaveAttempts(int allAttempt, int correctAttempt);

    ArithmeticTypes Operations { get; }
    void SaveOperations(ArithmeticTypes operations);
}