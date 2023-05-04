// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System;
using System.Runtime.Serialization;

namespace VDMJasminka.Core.Ambulance.Exceptions
{
    [Serializable]
    internal class CheckupItemOutOfStockException : Exception
    {
        public CheckupItemOutOfStockException()
        {
        }

        public CheckupItemOutOfStockException(string message) : base(message)
        {
        }

        public CheckupItemOutOfStockException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CheckupItemOutOfStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}