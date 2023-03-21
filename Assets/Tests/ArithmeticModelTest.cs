using NUnit.Framework;

public class ArithmeticModelTest
{
    class FakeStoreService : IStoreService
    {
        public int AllAttempt => 0;
        public int CorrectAttempt => 0;
        public void SaveAttempts(int allAttempt, int correctAttempt) { }

        public ArithmeticTypes Operations => ArithmeticTypes.Unknown;
        public void SaveOperations(ArithmeticTypes operations) { }
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
        ArithmeticModel model = new ArithmeticModel(10, 20, 1, 10, new IArithmeticStrategy[] { new AdditionStrategy() }, 
            new FakeRandomService(0, termOne, termTwo), new FakeStoreService());
        model.Start();

        Assert.AreEqual(expression, model.Expression);
        Assert.AreEqual(isSuccess, model.CheckResultAndNext(result));
    }

    [TestCase(8, 7, "15-7=?", 8, true)]
    [TestCase(6, 9, "15-9=?", 6, true)]
    [TestCase(5, 5, "10-5=?", 1, false)]
    public void ArithmeticModel_Subtraction(int termOne, int termTwo, string expression, int result, bool isSuccess)
    {
        ArithmeticModel model = new ArithmeticModel(10, 20, 1, 10, new IArithmeticStrategy[] { new SubtractionStrategy() },
            new FakeRandomService(0, termOne, termTwo), new FakeStoreService());
        model.Start();

        Assert.AreEqual(expression, model.Expression);
        Assert.AreEqual(isSuccess, model.CheckResultAndNext(result));
    }
}
