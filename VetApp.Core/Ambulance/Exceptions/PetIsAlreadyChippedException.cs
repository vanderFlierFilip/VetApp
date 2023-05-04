using System;
using VDMJasminka.Core.Ambulance.OwnerAggregate;

namespace VDMJasminka.Core.Ambulance.Exceptions
{
    public class PetIsAlreadyChippedException : Exception
    {
        public PetIsAlreadyChippedException(Pet pet) : base($"Pet with ID: {pet.Id} is already chipped! Chip: ({pet.Chip})")
        {
        }
    }
}