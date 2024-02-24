namespace GameUtils
{
    public class BigNumberHelper
    {
        internal static readonly char[] MagnitudeUnits = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        private static string _numberFormat = "{0:F3}";
        public static void Init(string format)
        {
            _numberFormat = format;
        }
        public static string BigNumberToString(BigNumber bigNumber)
        {
            if(bigNumber.Magnitude >=0)
            {
                return $"{string.Format(_numberFormat, bigNumber.Value)}{bigNumber.MagnitudeLabel}";
            }
            else
            {
                return bigNumber.Value.ToString();
            }
        }
    }
}
