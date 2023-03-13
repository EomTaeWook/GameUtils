namespace ShareLogic;

public class BigNumberHelper
{
    public static char[] UnitToString = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

    public static int DigitLength(int value)
    {
        var count = 0;
        while (value > 0)
        {
            value /= 10;
            count++;
        }
        return count;
    }
}
public struct BigNumber
{
    public float NumValue { get; set; }
    public string UnitValue { get; set; }

    public static BigNumber operator +(BigNumber number1, BigNumber number2)
    {
        var number = number1.NumValue + number2.NumValue;

        var digitLength = BigNumberHelper.DigitLength((int)number);

        int incUnit = Convert.ToInt32(number / 1000);

        if (digitLength > 3)
        {
            number -= 1000 * incUnit;
        }

        string unitValue = string.Empty;

        if(incUnit > 0)
        {
            unitValue += BigNumberHelper.UnitToString[incUnit];
        }
        //do
        //{

        //}
        //while (true);

        //if (string.IsNullOrEmpty(number1.UnitValue) == false)
        //{
        //    unitIndex =   - number1.UnitValue;
        //}

        //if(string.IsNullOrEmpty(number2.UnitValue) == false)
        //{

        //}

        return new BigNumber()
        {
            NumValue = number,
            UnitValue = unitValue
        };
    }
    
}
