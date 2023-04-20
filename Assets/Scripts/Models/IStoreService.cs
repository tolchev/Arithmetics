public interface IStoreService
{
    int AllAttempt { get; }
    int CorrectAttempt { get; }
    void SaveAttempts(int allAttempt, int correctAttempt);

    ArithmeticTypes Operations { get; }
    ArithmeticValueType ValueType { get; }
    void SaveProperties(ArithmeticTypes operations, ArithmeticValueType valueType);
}