using System;

namespace VDMJasminka.Core.Ambulance.Exceptions
{
    public class IncorrectCheckupDateException : Exception
    {
        public IncorrectCheckupDateException(string message) : base(message)
        {
        }
    }
}