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

        var arithmeticModel = new ArithmeticModel(new IArithmeticValue[] { new EasyArithmeticValue(), new HardArithmeticValue() },
            new IArithmeticStrategy[] { new AdditionStrategy(), new SubtractionStrategy() }, 
            new RandomService(), new PrefsStoreService());
        arithmeticPresenter = new ArithmeticPresenter(arithmeticView, arithmeticModel);
    }

    private void OnDestroy()
    {
        keyboardPresenter.Dispose();
        arithmeticPresenter.Dispose();
    }
}