public interface IArithmeticStrategy
{
    void GenerateTerms(out int termOne, out int termTwo);
    void SetTerms(int termOne, int termTwo);
    string GetExpression();
    bool GetResult(int value);
    ArithmeticTypes ArithmeticType { get; }
}