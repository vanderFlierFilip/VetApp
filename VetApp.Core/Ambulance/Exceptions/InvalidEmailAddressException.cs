using System;
using System.Runtime.Serialization;

namespace VDMJasminka.Core.Ambulance.Exceptions
{
    [Serializable]
    internal class InvalidEmailAddressException : Exception
    {
        public InvalidEmailAddressException()
        {
        }

        public InvalidEmailAddressException(string message) : base(message)
        {
        }

        public InvalidEmailAddressException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidEmailAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}