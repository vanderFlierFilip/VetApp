using System.Linq;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.MappingExtensions
{
    public static class OwnerMappingExtensions
    {
        public static OwnerModel ToModel(this Owner entity)
        {
            return new OwnerModel
            {
                Id = (int)entity.Id,
                FullName = entity.OwnerInformation.FullName,
                SSN = entity.OwnerInformation.SSN,
                AdditionalInfo = entity.OwnerInformation.AdditionalInfo,
                Address = entity.OwnerInformation.Address,
                PhoneNumber = entity.OwnerInformation.PhoneNumber,
                Municipality = entity.OwnerInformation.Municipality,
                Location = entity.OwnerInformation.Location,
                Pets = entity.Pets.ToList().Select(p => p.ToModel())

            };
        }

        public static PetModel ToModel(this Pet entity)
        {
            return new PetModel
            {
                PetId = entity.Id,
                Name = entity.PetInformation.Name,
                ChipNumber = entity.Chip.Number,
                DateOfBirth = entity.PetInformation.DateOfBirth,
                Sex = entity.PetInformation.Sex,
                Description = entity.PetInformation.Description,
                Alergy = entity.PetInformation.Alergy,
                LastCheckup = entity.LastVisit,
                SpeciesId = entity.SpeciesId,
                BreedId = entity.BreedId,
                Breed = entity.Breed,
                Species = entity.Species

            };
        }

    }
}
