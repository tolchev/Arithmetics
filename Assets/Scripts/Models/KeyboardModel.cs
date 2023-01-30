using System;

public class KeyboardModel 
{
    private readonly int maxLength;
    private string result;

    public KeyboardModel(int maxLength)
    {
        this.maxLength = maxLength;
        Reset();
    }

    public string ResultText => result;

    public int Result => Recalc();

    public void DigitPress(int key)
    {
        if (key < 0 || key > 9)
        {
            throw new ArgumentOutOfRangeException(nameof(key));
        }

        if (result.Length >= maxLength)
        {
            throw new OverflowException();
        }

        result += key.ToString();
        Recalc();
    }
    
    public void BackspacePress()
    {
        if (result.Length > 0)
        {
            result = result.Substring(0, result.Length - 1);
        }
        Recalc();
    }

    public void Reset()
    {
        result = string.Empty;
    }

    private int Recalc()
    {
        int res = 0;
        if (result != string.Empty)
        {
            res = int.Parse(result);
            result = res.ToString();
        }
        return res;
    }
}
