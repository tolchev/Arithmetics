using UnityEngine;

public class CompositionRoot : MonoBehaviour
{
    [SerializeField]
    private KeyboardView keyboardView;
    [SerializeField]
    private ArithmeticView arithmeticView;

    private KeyboardPresenter keyboardPresenter;
    private ArithmeticPresenter arithmeticPresenter;

    void Start()
    {
        var keyboardModel = new KeyboardModel(2);
        keyboardPresenter = new KeyboardPresenter(keyboardView, keyboardModel);

        var arithmeticValue = new IArithmeticValue[] { new EasyArithmeticValue(), new HardArithmeticValue() };
        var randomService = new RandomService();
        var prefsStoreService = new PrefsStoreService();

        var additionStrategy = new AdditionStrategy(arithmeticValue, randomService, prefsStoreService);
        var subtractionStrategy = new SubtractionStrategy(arithmeticValue, randomService, prefsStoreService);
        var multiplicationStrategy = new MultiplicationStrategy(randomService);

        var arithmeticModel = new ArithmeticModel(new IArithmeticStrategy[] { additionStrategy, subtractionStrategy, multiplicationStrategy },
            randomService, prefsStoreService);
        arithmeticPresenter = new ArithmeticPresenter(arithmeticView, arithmeticModel);
    }

    private void OnDestroy()
    {
        keyboardPresenter.Dispose();
        arithmeticPresenter.Dispose();
    }
}