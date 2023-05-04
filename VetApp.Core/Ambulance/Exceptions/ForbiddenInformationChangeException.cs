using System;

namespace VDMJasminka.Core.Ambulance.Exceptions
{
    public class ForbiddenInformationChangeException : Exception
    {
        public ForbiddenInformationChangeException(string context)
            : base($"The property {context} is already set and is forbidden to change!")
        {
        }
    }
}