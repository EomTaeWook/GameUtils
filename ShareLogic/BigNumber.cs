using System;
using System.Text;

namespace GameUtils
{
    public struct BigNumber
    {
        public double Value { get; private set; }
        public int Magnitude { get; private set; }
        public string MagnitudeLabel { get; private set; }

        public string DisplayValue
        {
            get
            {
                if (string.IsNullOrEmpty(_displayValue))
                {
                    BigNumberHelper.BigNumberToString(this);
                }
                return _displayValue;
            }
        }

        private readonly string _displayValue;
        public BigNumber(double value) : this(value, -1)
        {
        }
        public BigNumber(double value, int digit = -1)
        {
            while (value >= 1000)
            {
                value /= 1000;
                digit++;
            }
            Magnitude = digit;
            Value = value;

            var sb = new StringBuilder();
            while (digit >= 0)
            {
                sb.Insert(0, BigNumberHelper.MagnitudeUnits[digit % BigNumberHelper.MagnitudeUnits.Length]);
                digit = digit / BigNumberHelper.MagnitudeUnits.Length - 1;
            }
            MagnitudeLabel = sb.ToString();
            _displayValue = string.Empty;
        }
        public static BigNumber operator +(BigNumber number1, BigNumber number2)
        {
            double convertedValue1 = number1.Value;
            double convertedValue2 = number2.Value;
            int finalMagnitude = number1.Magnitude;
            if (number1.Magnitude > number2.Magnitude)
            {
                for (int i = 0; i < (number1.Magnitude - number2.Magnitude); i++)
                {
                    convertedValue2 /= 1000;
                }
            }
            else if (number1.Magnitude < number2.Magnitude)
            {
                for (int i = 0; i < (number2.Magnitude - number1.Magnitude); i++)
                {
                    convertedValue1 /= 1000;
                }
                finalMagnitude = number2.Magnitude;
            }
            var value = convertedValue1 + convertedValue2;

            return new BigNumber(value, finalMagnitude);
        }
        public static BigNumber operator -(BigNumber number1, BigNumber number2)
        {
            double convertedValue1 = number1.Value;
            double convertedValue2 = number2.Value;
            int finalMagnitude = number1.Magnitude;

            if (number1.Magnitude > number2.Magnitude)
            {
                for (int i = 0; i < (number1.Magnitude - number2.Magnitude); i++)
                {
                    convertedValue2 /= 1000;
                }
            }
            else if (number1.Magnitude < number2.Magnitude)
            {
                for (int i = 0; i < (number2.Magnitude - number1.Magnitude); i++)
                {
                    convertedValue1 /= 1000;
                }
                finalMagnitude = number2.Magnitude;
            }

            var value = convertedValue1 - convertedValue2;
            if (Math.Abs(value) < 1)
            {
                value *= 1000;
                finalMagnitude--;
            }
            return new BigNumber(value, finalMagnitude);
        }
    }
}
