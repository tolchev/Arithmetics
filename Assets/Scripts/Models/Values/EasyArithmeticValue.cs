public class EasyArithmeticValue : IArithmeticValue
{
    public ArithmeticValueType ArithmeticValueType => ArithmeticValueType.Easy;

    public int MinGeneratedResult => 10;

    public int MaxGeneratedResult => 20;

    public int MinTermValue => 1;

    public int MaxTermValue => 10;
}