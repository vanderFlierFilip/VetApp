using System;

namespace VDMJasminka.Core.Ambulance.Exceptions
{
    public class ChipNumberIsNotCorrectLengthException : Exception
    {
        public ChipNumberIsNotCorrectLengthException(string message) : base(message)
        {
        }
    }
}