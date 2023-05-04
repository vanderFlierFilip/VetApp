using MediatR;
using System;

namespace VDMJasminka.Application.Ambulance.Commands
{
    public class ChangePetDescriptionAndAlergy : IRequest
    {
        public ChangePetDescriptionAndAlergy(int ownerId, int petId, string name, DateTime? dateOfBirth, string sex, string description, string alergy)
        {
            OwnerId = ownerId;
            PetId = petId;
            Name = name;
            DateOfBirth = dateOfBirth;
            Sex = sex;
            Description = description;
            Alergy = alergy;
        }

        public int OwnerId { get; }
        public int PetId { get; }
        public string Name { get; }
        public DateTime? DateOfBirth { get; }
        public string Sex { get; }
        public string Description { get; }
        public string Alergy { get; }
    }
}
