public interface IArithmeticStrategy
{
    void SetTerms(int termOne, int termTwo);
    string GetExpression();
    bool GetResult(int value);
}