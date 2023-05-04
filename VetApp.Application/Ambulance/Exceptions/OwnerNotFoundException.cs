using System;

namespace VDMJasminka.Application.Ambulance.Exceptions
{
    public class OwnerNotFoundException : Exception
    {
        public OwnerNotFoundException() : base()
        {
        }

        public OwnerNotFoundException(string message) : base(message)
        {
        }

        public OwnerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
