public class HardArithmeticValue : IArithmeticValue
{
    public ArithmeticValueType ArithmeticValueType => ArithmeticValueType.Hard;

    public int MinGeneratedResult => 21;

    public int MaxGeneratedResult => 99;

    public int MinTermValue => 10;

    public int MaxTermValue => 90;
}