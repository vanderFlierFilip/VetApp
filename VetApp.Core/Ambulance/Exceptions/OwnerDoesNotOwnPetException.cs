using System;

namespace VDMJasminka.Core.Ambulance.Exceptions
{
    public class OwnerDoesNotOwnPetException : Exception
    {
        public OwnerDoesNotOwnPetException(int petId) : base($"Owner doesn't own pet with ID: {petId}")
        {
        }
    }
}