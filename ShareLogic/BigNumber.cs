using System.Text;

namespace GameUtils 
{
    public struct BigNumber
    {
        public static readonly char[] Unit = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public double NumValue { get; private set; }
        public int Digit { get; private set; }
        public string UnitToString { get; private set; }

        public BigNumber(double value, int digit)
        {
            var count = 0;
            long lNumValue;
            do
            {
                lNumValue = (long)(value /= 1000);
                count++;
            } while (lNumValue >= 1000);

            digit = digit + count - 1;
            Digit = digit;
            NumValue = value;

            var sb = new StringBuilder();
            do
            {
                sb.Insert(0, Unit[digit % Unit.Length]);
                digit = digit / Unit.Length - 1;
            } while (digit >= 0);

            UnitToString = sb.ToString();
        }
        public override string ToString()
        {
            return $"{string.Format("{0:F2}", NumValue)}{UnitToString}";
        }
        public static BigNumber operator +(BigNumber number1, BigNumber number2)
        {
            return new BigNumber();
        }
    }

}
