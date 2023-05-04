using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Ambulance.Events
{
    public class PetRegisteredToOwner : IDomainEvent
    {
        public Owner Owner { get; }
        public Pet Pet { get; }

        public PetRegisteredToOwner(Owner owner, Pet pet)
        {
            Owner = owner;
            Pet = pet;
        }
    }
}