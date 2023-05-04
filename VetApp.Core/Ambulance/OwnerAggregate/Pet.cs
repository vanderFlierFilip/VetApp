using System;
using System.Collections.Generic;
using VDMJasminka.Core.Ambulance.Exceptions;
using VDMJasminka.Core.Ambulance.Services;
using VDMJasminka.Core.Ambulance.ValueObjects;
using VDMJasminka.Core.Common;
using VDMJasminka.Core.Entities;

namespace VDMJasminka.Core.Ambulance.OwnerAggregate
{
    public class Pet : Entity
    {
        private Pet()
        {
        }

        public Pet(int ownerId, int animalTypeId, int breedId, string name, DateTime? dateOfBirth, string sex, string description, string alergy, string chipNumber)
        {
            OwnerId = ownerId;
            SpeciesId = animalTypeId;
            BreedId = breedId;
            Chip = new Chip(chipNumber);
            PetInformation = new PetInfo(name, dateOfBirth, sex, description, alergy);
        }

        public Pet(int? id, int ownerId, int animalTypeId, int breedId, string name, DateTime? dateOfBirth, string sex, string description, string alergy, string chipNumber)
        {
            Id = (int)id;
            OwnerId = ownerId;
            SpeciesId = animalTypeId;
            BreedId = breedId;
            Chip = new Chip(chipNumber);
            PetInformation = new PetInfo(name, dateOfBirth, sex, description, alergy);
        }

        public override int Id { get; protected set; }
        public int? OwnerId { get; private set; }
        public int SpeciesId { get; private set; }
        public int BreedId { get; private set; }
        public DateTime? LastVisit { get; private set; }

        public Chip Chip { get; private set; }
        public PetInfo PetInformation { get; private set; }

        public virtual Breed Breed { get; set; }
        public virtual Species Species { get; set; }
        public IReadOnlyCollection<Checkup> Checkups => _checkups;

        private List<Checkup> _checkups = new List<Checkup>();

        public void ChangeAlergyAndDescription(string alergy, string description)
        {
            PetInformation.UpdateDescriptionAndAlergy(description, alergy);
        }

        public void Vaccinate(int petId, DateTime dateOfVaccination, string typeOfVaccine, double priceOfCheckup, List<CheckupItem> checkupDetails, DateTime nextVaccinationDate, ICheckupItemInventoryService invetoryService)
        {
            var vaccination = Checkup.CreateVaccinationCheckup(petId, dateOfVaccination, typeOfVaccine, priceOfCheckup);

            AddItemsToCheckup(ref vaccination, checkupDetails, invetoryService);

            vaccination.UpdateNextVaccination(nextVaccinationDate);

            UpdateLastCheckup((DateTime)vaccination.Date);

            _checkups.Add(vaccination);
        }

        public void InsertChip(int petId, DateTime date, double checkupPrice, string chipNumber, List<CheckupItem> checkupItems, ICheckupItemInventoryService inventoryService)
        {
            if (!string.IsNullOrEmpty(Chip?.Number))
            {
                throw new PetIsAlreadyChippedException(this);
            }

            var chip = new Chip(chipNumber);

            var chippingCheckup = Checkup.CreateChipInsertionCheckup(petId, date, checkupPrice);

            AddItemsToCheckup(ref chippingCheckup, checkupItems, inventoryService);

            UpdateLastCheckup((DateTime)chippingCheckup.Date);

            _checkups.Add(chippingCheckup);

            Chip = chip;
        }

        public void PerformSurgicalCheckup(DateTime dateOfSurgery, string anamnesis, string clinicalPicture, string diagnosis, string surgicalIntervention, string labAnalysis, string reccommendation, DateTime? controlCheckupDate, List<CheckupItem> checkupItems, double? checkupPrice, string recommendedTherapy, ICheckupItemInventoryService inventoryService)
        {
            var checkup = Checkup.CreateSurgicalInterventionCheckup((int)Id, dateOfSurgery, anamnesis, clinicalPicture, diagnosis, surgicalIntervention, labAnalysis, reccommendation, controlCheckupDate, checkupPrice, recommendedTherapy);

            AddItemsToCheckup(ref checkup, checkupItems, inventoryService);

            UpdateLastCheckup((DateTime)checkup.Date);

            _checkups.Add(checkup);
        }

        public void PerformTreatment(DateTime checkupDate, string anamnesis, string clinicalPicture, string diagnosis, string labAnalysis, string reccommendation, DateTime? controlCheckupDate, List<CheckupItem> checkupItems, double? paidSum, string reccomendedTherapy, ICheckupItemInventoryService inventoryService)
        {
            var checkup = Checkup.CreateTreatmentCheckup((int)Id, checkupDate, anamnesis, clinicalPicture, diagnosis, labAnalysis, reccommendation, controlCheckupDate, paidSum, reccomendedTherapy);

            AddItemsToCheckup(ref checkup, checkupItems, inventoryService);

            UpdateLastCheckup(checkupDate);

            _checkups.Add(checkup);
        }

        public string CalculateAccurateAgeOnGivenDate(DateTime givenDate)
        {
            if (givenDate == null) throw new ArgumentNullException(nameof(givenDate));

            if (!PetInformation.DateOfBirth.HasValue) return "Податокот не е достапен";

            DateTime dob = (DateTime)PetInformation.DateOfBirth;
            int months = givenDate.Month - dob.Month;
            int years = givenDate.Year - dob.Year;

            if (givenDate.Day < dob.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            int days = (givenDate - dob.AddMonths((years * 12) + months)).Days;
            int weeks = days / 7;

            string yearsString = years == 0 ? "" : $"{years} год, ";
            string monthsString = months == 0 ? "" : months == 1 ? $"{months} месец, " : $"{months} месеци, ";
            string weeksString = weeks == 0 ? "" : weeks == 1 ? $"{weeks} недела, " : $"{weeks} недели, ";
            string daysString = days == 0 ? "" : days == 1 ? $"{days} ден." : $"{days} дена.";
            return yearsString + monthsString + weeksString + daysString;
        }

        public int CalculateAgeInWeeks()
        {
            DateTime today = DateTime.Today;
            var dob = PetInformation.DateOfBirth ?? DateTime.Today.AddYears(-2);

            return (int)(today.Date - dob.Date).TotalDays / 7;
        }

        private void AddItemsToCheckup(ref Checkup checkup, List<CheckupItem> checkupItems, ICheckupItemInventoryService inventoryService)
        {
            foreach (var item in checkupItems)
            {
                checkup.AddCheckupItem(item.MedicamentId, item.Uom, item.MedicamentApplication, (double)item.Price, inventoryService, (double)item.Amount);
            }
        }

        private void UpdateLastCheckup(DateTime checkupDate)
        {
            LastVisit = checkupDate;
        }
    }
}