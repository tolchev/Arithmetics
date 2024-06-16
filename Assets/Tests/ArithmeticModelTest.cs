using NUnit.Framework;

public class ArithmeticModelTest
{
    class FakeStoreService : IStoreService
    {
        public FakeStoreService(ArithmeticTypes operations)
        {
            Operations = operations;
        }

        public int AllAttempt => 0;
        public int CorrectAttempt => 0;
        public void SaveAttempts(int allAttempt, int correctAttempt) { }

        public ArithmeticTypes Operations { get; }
        public ArithmeticValueType ValueType => ArithmeticValueType.Easy;
        public void SaveProperties(ArithmeticTypes operations, ArithmeticValueType valueType) { }
    }

    class FakeRandomService : IRandomService
    {
        private readonly int[] values;

        private int index = -1;

        public FakeRandomService(params int[] values)
        {
            this.values = values;
        }

        public int Range(int minInclusive, int maxExclusive)
        {
            index = (index + 1) % values.Length;
            return values[index];
        }
    }

    [TestCase(7, 4, "7+4=?", 11, true)]
    [TestCase(6, 8, "6+8=?", 14, true)]
    [TestCase(9, 1, "9+1=?", 1, false)]
    public void ArithmeticModel_Addition(int termOne, int termTwo, string expression, int result, bool isSuccess)
    {
        var arithmeticValue = new IArithmeticValue[] { new EasyArithmeticValue() };
        var randomService = new FakeRandomService(0, termOne, termTwo);
        var prefsStoreService = new FakeStoreService(ArithmeticTypes.Addition);

        var additionStrategy = new AdditionStrategy(arithmeticValue, randomService, prefsStoreService);

        ArithmeticModel model = new ArithmeticModel(new IArithmeticStrategy[] { additionStrategy },
            randomService, prefsStoreService);
        model.Start();

        Assert.AreEqual(expression, model.Expression);
        Assert.AreEqual(isSuccess, model.CheckResultAndNext(result));
    }

    [TestCase(8, 7, "15-7=?", 8, true)]
    [TestCase(6, 9, "15-9=?", 6, true)]
    [TestCase(5, 5, "10-5=?", 1, false)]
    public void ArithmeticModel_Subtraction(int termOne, int termTwo, string expression, int result, bool isSuccess)
    {
        var arithmeticValue = new IArithmeticValue[] { new EasyArithmeticValue() };
        var randomService = new FakeRandomService(0, termOne, termTwo);
        var prefsStoreService = new FakeStoreService(ArithmeticTypes.Subtraction);

        var subtractionStrategy = new SubtractionStrategy(arithmeticValue, randomService, prefsStoreService);

        ArithmeticModel model = new ArithmeticModel(new IArithmeticStrategy[] { subtractionStrategy },
            randomService, prefsStoreService);
        model.Start();

        Assert.AreEqual(expression, model.Expression);
        Assert.AreEqual(isSuccess, model.CheckResultAndNext(result));
    }

    [TestCase(3, 2, "3*2=?", 6, true)]
    [TestCase(7, 5, "7*5=?", 35, true)]
    [TestCase(8, 3, "8*3=?", 11, false)]
    public void ArithmeticModel_Multiplication(int termOne, int termTwo, string expression, int result, bool isSuccess)
    {
        var randomService = new FakeRandomService(0, termOne, termTwo);

        ArithmeticModel model = new ArithmeticModel(new IArithmeticStrategy[] { new MultiplicationStrategy(randomService) },
            randomService, new FakeStoreService(ArithmeticTypes.Multiplication));
        model.Start();

        Assert.AreEqual(expression, model.Expression);
        Assert.AreEqual(isSuccess, model.CheckResultAndNext(result));
    }
}
