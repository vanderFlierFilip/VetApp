using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Ambulance.Events
{
    public class PetCheckupSuccessfullyCompleted
        : IDomainEvent
    {
        public PetCheckupSuccessfullyCompleted(Owner owner, Pet pet, Checkup checkupResult)
        {
            Owner = owner;
            Pet = pet;
            CheckupResult = checkupResult;
        }

        public Owner Owner { get; }
        public Pet Pet { get; }
        public Checkup CheckupResult { get; }
    }
}