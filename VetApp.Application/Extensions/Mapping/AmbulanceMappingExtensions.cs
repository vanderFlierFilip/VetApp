using System;
using System.Collections.Generic;
using System.Linq;
using VDMJasminka.Core;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Entities;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Extensions.Mapping
{
    public static class AmbulanceMappingExtensions
    {
        public static IEnumerable<CheckupItem> ToEntityEnumerable(this List<CreateCheckupCheckupItemsDto> list)
        {
            foreach (var item in list)
            {
                yield return new CheckupItem(item.MedicamentId, item.Amount, item.Uom, item.MedicamentApplication, item.Price);
            }
        }

        public static ReadOwnerDto ToReadDto(this Owner owner)
        {
            return new ReadOwnerDto
            {
                Id = owner.Id,
                FullName = owner.OwnerInformation.FullName,
                AdditionalInfo = owner.OwnerInformation.AdditionalInfo,
                Address = owner.OwnerInformation.Address,
                Location = owner.OwnerInformation.Location,
                Municipality = owner.OwnerInformation.Municipality,
                PhoneNumber = owner.OwnerInformation.PhoneNumber,
                SSN = owner.OwnerInformation.SSN,
                Email = owner.OwnerInformation.Email,
                Pets = owner?.Pets.Select(x => x.ToReadDto())
            };
        }

        public static ReadPetDto ToReadDto(this Pet pet)
        {
            return new ReadPetDto
            {
                Name = pet.PetInformation.Name,
                DateOfBirth = pet.PetInformation.DateOfBirth,
                Alergy = pet.PetInformation.Alergy,
                ChipNumber = pet.Chip?.Number,
                Description = pet.PetInformation.Description,
                Sex = pet.PetInformation.Sex,
                Id = pet.Id,
                AccurateAge = pet.CalculateAccurateAgeOnGivenDate(pet.PetInformation.DateOfBirth.Value),
            };
        }

        public static CreateOwnerDto ToDto(this Owner owner)
        {
            return new CreateOwnerDto
            {
                FullName = owner.OwnerInformation.FullName,
                AdditionalInfo = owner.OwnerInformation.AdditionalInfo,
                Address = owner.OwnerInformation.Address,
                Location = owner.OwnerInformation.Location,
                Municipality = owner.OwnerInformation.Municipality,
                PhoneNumber = owner.OwnerInformation.PhoneNumber,
                SSN = owner.OwnerInformation.SSN,
                Email = owner.OwnerInformation.Email,
                Pets = owner?.Pets.Select(p => p?.ToDto()).ToList()
            };
        }

        public static CreatePetDto ToDto(this Pet pet)
        {
            return new CreatePetDto
            {
                Name = pet.PetInformation.Name,
                DateOfBirth = pet.PetInformation.DateOfBirth,
                Alergy = pet.PetInformation.Alergy,
                ChipNumber = pet.Chip?.Number,
                Description = pet.PetInformation.Description,
                Sex = pet.PetInformation.Sex,
            };
        }

        public static CheckupDto ToDto(this Checkup checkup)
        {
            return new CheckupDto()
            {
                Id = checkup.Id,
                PetId = checkup.PetId,
                CheckupType = checkup.CheckupType,
                AccurateAgeOnCheckupDate = checkup.Pet.CalculateAccurateAgeOnGivenDate((DateTime)checkup.Date),
                NextControlCheckup = checkup.NextControlCheckup,
                Advice = checkup.Advice,
                Anamnesis = checkup.Anamnesis,
                LabAnalysis = checkup.LabAnalysis,
                ClinicalPicture = checkup.ClinicalPicture,
                Date = checkup.Date,
                Diagnosis = checkup.Diagnosis,
                PaidSum = checkup.PaidSum,
                SurgicalIntervention = checkup.SurgicalIntervention,
                NextVaccinationDate = checkup.NextVaccinationDate,
                Therapy = checkup.Therapy,
                Vaccine = checkup.Vaccine,
                CheckupDetails = checkup.CheckupItems.Select(c => c.ToDto()).ToList()
            };
        }

        public static CheckupDetailsDto ToDto(this CheckupItem item)
        {
            return new CheckupDetailsDto()
            {
                Amount = item.Amount,
                Application = item.MedicamentApplication,
                MedicamentId = item.MedicamentId,
                Uom = item.Uom,
                Name = item.Medicament?.MedicamentDetails.Name
            };
        }

        public static MedicamentDto ToDto(this Medicament medicament)
        {
            return new MedicamentDto()
            {
                Id = medicament.Id,
                Name = medicament.MedicamentDetails.Name,
                Uom = medicament.MedicamentDetails.Uom,
                MinimalAmount = medicament.GetMinimalAmount(),
                IsMaterial = medicament.MedicamentDetails.IsMaterial,
                Price = medicament.Price.Value,
            };
        }

        public static BreedDto ToDto(this Breed breed)
        {
            return new BreedDto()
            {
                Id = (int)breed.Id,
                Name = breed.Name
            };
        }

        public static SpeciesDto ToDto(this Species species)
        {
            return new SpeciesDto()
            {
                Id = (int)species.Id,
                Name = species.Type,
                //Breeds = species.Breeds.Select(x => x.ToDto()).ToList()
            };
        }
    }
}