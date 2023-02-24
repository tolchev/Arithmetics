public interface IStoreService
{
    int AllAttempt { get; }
    int CorrectAttempt { get; }
    void Save(int allAttempt, int correctAttempt);
}