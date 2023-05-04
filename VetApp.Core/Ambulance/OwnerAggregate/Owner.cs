// Code scaffolded by EF Core assumes nullable reference types ColumnNRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.Events;
using VDMJasminka.Core.Ambulance.Exceptions;
using VDMJasminka.Core.Ambulance.Services;
using VDMJasminka.Core.Ambulance.ValueObjects;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Ambulance.OwnerAggregate
{
    public class Owner : Entity, IAggregateRoot
    {
        private Owner()
        {
        }

        public Owner(string fullName,
                         string address,
                         string location,
                         string phoneNumber,
                         string municipality,
                         string sSN,
                         decimal? state,
                         string additionalInfo,
                         string email)
        {
            OwnerInformation = new OwnerInfo(fullName, address, location, phoneNumber, municipality, sSN, additionalInfo, email);
            IsDeleted = false;
        }

        public override int Id { get; protected set; }

        public OwnerInfo OwnerInformation { get; private set; }

        public IReadOnlyCollection<Pet> Pets => _pets;

        private List<Pet> _pets = new List<Pet>();

        public void PerformTreatmentOnPet(int petId, DateTime checkupDate, string anamnesis, string clinicalPicture, string diagnosis, string labAnalysis, string reccommendation, DateTime? controlCheckupDate, List<CheckupItem> checkupItems, double? paidSum, string reccommendedTherapy, ICheckupItemInventoryService inventoryService)
        {
            var pet = GetPet(petId);

            pet.PerformTreatment(checkupDate, anamnesis, clinicalPicture, diagnosis, labAnalysis, reccommendation, controlCheckupDate, checkupItems, paidSum, reccommendedTherapy, inventoryService);

            AddDomainEvent(new PetCheckupSuccessfullyCompleted(this, pet, GetCheckupForPet(pet)));
        }

        public async Task VaccinatePet(int petId, DateTime dateOfVaccination, string typeOfVaccine, double priceOfCheckup, List<CheckupItem> checkupItems, DateTime? nextVaccinationDate, IVaccinationDateService vaccinationDateService, ICheckupItemInventoryService inventoryService)
        {
            var pet = GetPet(petId);

            DateTime nextVaccine;

            if (nextVaccinationDate != null)
            {
                nextVaccine = nextVaccinationDate.Value;
            }
            else
            {
                nextVaccine = await vaccinationDateService.GetNextVaccinationDateForPet((int)Id, petId);
            }

            pet.Vaccinate(petId, dateOfVaccination, typeOfVaccine, priceOfCheckup, checkupItems, nextVaccine, inventoryService);

            AddDomainEvent(new PetCheckupSuccessfullyCompleted(this, pet, GetCheckupForPet(pet)));
        }

        public void PerformSurgeryOnPet(int petId, DateTime dateOfSurgery, string anamnesis, string clinicalPicture, string diagnosis, string surgicalIntervention, string labAnalysis, string reccommendation, DateTime? controlCheckupDate, List<CheckupItem> medicineUsed, double? checkupPrice, string recommendedTherapy, ICheckupItemInventoryService inventoryService)
        {
            var pet = GetPet(petId);

            pet.PerformSurgicalCheckup(dateOfSurgery, anamnesis, clinicalPicture, diagnosis, surgicalIntervention, labAnalysis, reccommendation, controlCheckupDate, medicineUsed, checkupPrice, recommendedTherapy, inventoryService);

            AddDomainEvent(new PetCheckupSuccessfullyCompleted(this, pet, GetCheckupForPet(pet)));
        }

        public void ChipPet(int petId, DateTime dateOfChipping, string chipNumber, double paidSum, List<CheckupItem> checkupItems, ICheckupItemInventoryService inventoryService)
        {
            Pet pet = GetPet(petId);

            pet.InsertChip(pet.Id, dateOfChipping, paidSum, chipNumber, checkupItems, inventoryService);

            AddDomainEvent(new PetCheckupSuccessfullyCompleted(this, pet, GetCheckupForPet(pet)));
        }

        public void RegisterNewPet(string name, int animalTypeId, int breedId, DateTime? dateOfBirth, string sex, string description, string alergy, string chipNumber)
        {
            var pet = new Pet(Id, animalTypeId, breedId, name, dateOfBirth, sex, description, alergy, chipNumber);

            _pets.Add(pet);

            AddDomainEvent(new PetRegisteredToOwner(this, pet));
        }

        public void ChangeEmail(string emailAddress)
        {
            OwnerInformation.UpdateEmail(emailAddress);

            AddDomainEvent(new OwnerEmailChanged(OwnerInformation.Email));
        }

        public void ChangeLivingAddress(string address, string location, string municipality)
        {
            OwnerInformation.UpdateLivingAddress(address, location, municipality);

            AddDomainEvent(new OwnerLivingAddressChanged(OwnerInformation.Address, OwnerInformation.Location, OwnerInformation.Municipality));
        }

        public void ChangePetDescriptionAndAlergies(int petId, string alergy, string description)
        {
            var pet = GetPet(petId);

            pet.ChangeAlergyAndDescription(alergy, description);

            AddDomainEvent(new PetDescriptionAndAlergyChanged(pet));
        }

        public Pet GetPet(int petId)
        {
            var pet = _pets.FirstOrDefault(pet => pet.Id == petId);

            if (pet == null) throw new OwnerDoesNotOwnPetException(petId);

            return pet;
        }

        public bool IsDeleted { get; private set; }

        public void Archive()
        {
            IsDeleted = true;
            AddDomainEvent(new OwnerArchived(this));
        }

        public void Restore()
        {
            IsDeleted = false;
            AddDomainEvent(new OwnerRestoredFromArchive(this));
        }

        private Checkup GetCheckupForPet(Pet pet)
        {
            return pet.Checkups.OrderByDescending(d => d.Date).FirstOrDefault();
        }
    }
}