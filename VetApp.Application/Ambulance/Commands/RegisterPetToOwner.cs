using MediatR;
using System;

namespace VDMJasminka.Application.Ambulance.Commands
{
    public class RegisterPetToOwner : IRequest
    {
        public RegisterPetToOwner(int ownerId, string name, int animalTypeId, int breedId, DateTime? dateOfBirth, string sex, string description, string alergy, string chipNumber)
        {
            OwnerId = ownerId;
            Name = name;
            AnimalTypeId = animalTypeId;
            BreedId = breedId;
            DateOfBirth = dateOfBirth;
            Sex = sex;
            Description = description;
            Alergy = alergy;
            ChipNumber = chipNumber;
        }

        public int OwnerId { get; }
        public string Name { get; }
        public int AnimalTypeId { get; }
        public int BreedId { get; }
        public DateTime? DateOfBirth { get; }
        public string Sex { get; }
        public string Description { get; }
        public string Alergy { get; }
        public string ChipNumber { get; internal set; }
    }
}
