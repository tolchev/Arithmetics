public interface IArithmeticValue
{
    ArithmeticValueType ArithmeticValueType { get; }
    int MinGeneratedResult { get; }
    int MaxGeneratedResult { get; }
    int MinTermValue { get; }
    int MaxTermValue { get; }
}