using System;

namespace Manually_Throwing_Custom_Exception
{
    public class NumberFormatException : Exception
    {
        public NumberFormatException(string varName) : base(varName) { }
    }

    public class StringFormatException : Exception
    {
        public StringFormatException(string varName) : base(varName) { }
    }

    public class CurrencyFormatException : Exception
    {
        public CurrencyFormatException(string varName) : base(varName) { }
    }
}
