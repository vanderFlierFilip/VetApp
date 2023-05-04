// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using VDMJasminka.Core.Ambulance.Events;
using VDMJasminka.Core.Ambulance.Exceptions;
using VDMJasminka.Core.Ambulance.Services;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Ambulance.OwnerAggregate
{
    public class Checkup : Entity
    {
        public Checkup()
        {
            //CheckupItems = new HashSet<CheckupItem>();
        }

        public Checkup(
            DateTime? date,
            int petId,
            string checkupType,
            string anamnesis = "",
            string clinicalPicture = "",
            string diagnosis = "",
            string therapy = "",
            string surgicalIntervention = "",
            string advice = "",
            double paidSum = 0,
            string vaccine = "",
            string labAnalysis = "")
        {
            Date = date;
            PetId = petId;
            Anamnesis = anamnesis;
            ClinicalPicture = clinicalPicture;
            Diagnosis = diagnosis;
            Therapy = therapy;
            SurgicalIntervention = surgicalIntervention;
            Advice = advice;
            PaidSum = paidSum;
            Vaccine = vaccine;
            CheckupType = checkupType;
            LabAnalysis = labAnalysis;
        }

        protected Checkup(int petId, DateTime date, string typeOfVaccine, double priceOfCheckup)
        {
            PetId = petId;
            Date = date;
            Vaccine = typeOfVaccine;
            PaidSum = priceOfCheckup;
            CheckupType = CheckupCategory.Vaccine;
        }

        private Checkup(int petId, DateTime date, double priceOfCheckup)
        {
            PetId = petId;
            Date = date;
            PaidSum = priceOfCheckup;
            CheckupType = CheckupCategory.Chipping;
        }

        public Checkup(int petId, DateTime dateOfSurgery, string anamnesis, string clinicalPicture, string diagnosis, string surgicalIntervention, string labAnalysis, string reccommendation, DateTime? controlCheckupDate, double? checkupPrice, string reccomendedTherapy)
        {
            PetId = petId;
            Date = dateOfSurgery;
            Anamnesis = anamnesis;
            ClinicalPicture = clinicalPicture;
            Diagnosis = diagnosis;
            SurgicalIntervention = surgicalIntervention;
            LabAnalysis = labAnalysis;
            Advice = reccommendation;
            NextControlCheckup = controlCheckupDate;
            CheckupType = CheckupCategory.Surgery;
            PaidSum = checkupPrice;
            Therapy = reccomendedTherapy;
        }

        private Checkup(int petId, DateTime checkupDate, string anamnesis, string clinicalPicture, string diagnosis, string labAnalysis, string reccommendation, DateTime? controlCheckupDate, double? paidSum, string reccommendedTherapy)
        {
            PetId = petId;
            Date = checkupDate;
            Anamnesis = anamnesis;
            ClinicalPicture = clinicalPicture;
            Diagnosis = diagnosis;
            LabAnalysis = labAnalysis;
            Advice = reccommendation;
            NextControlCheckup = controlCheckupDate;
            CheckupType = CheckupCategory.Treatment;
            PaidSum = paidSum;
            Therapy = reccommendedTherapy;
        }

        public override int Id { get; protected set; }
        public DateTime? Date { get; private set; }
        public int PetId { get; private set; }
        public string Anamnesis { get; private set; }
        public string ClinicalPicture { get; private set; }
        public string Diagnosis { get; private set; }
        public string Therapy { get; set; }
        public string SurgicalIntervention { get; private set; }
        public string Advice { get; private set; }
        public DateTime? NextControlCheckup { get; private set; }
        public double? PaidSum { get; private set; }
        public string Vaccine { get; private set; }
        public DateTime? NextVaccinationDate { get; private set; }
        public string CheckupType { get; private set; }
        public string LabAnalysis { get; private set; }

        public static Checkup CreateVaccinationCheckup(int petId, DateTime dateOfVaccination, string typeOfVaccine, double priceOfCheckup)
        {
            CheckDateValidity(dateOfVaccination);
            return new Checkup(petId, dateOfVaccination, typeOfVaccine, priceOfCheckup);
        }

        public static Checkup CreateChipInsertionCheckup(int petId, DateTime date, double priceOfCheckup)
        {
            return new Checkup(petId, date, priceOfCheckup);
        }

        public static Checkup CreateSurgicalInterventionCheckup(int petId, DateTime dateOfSurgery, string anamnesis, string clinicalPicture, string diagnosis, string surgicalIntervention, string labAnalysis, string reccommendation, DateTime? controlCheckupDate, double? checkupPrice, string recommendedTherapy)
        {
            return new Checkup(petId, dateOfSurgery, anamnesis, clinicalPicture, diagnosis, surgicalIntervention, labAnalysis, reccommendation, controlCheckupDate, checkupPrice, recommendedTherapy);
        }

        public static Checkup CreateTreatmentCheckup(int petId, DateTime checkupDate, string anamnesis, string clinicalPicture, string diagnosis, string labAnalysis, string reccommendation, DateTime? controlCheckupDate, double? paidSum, string reccomendedTherapy)
        {
            return new Checkup(petId, checkupDate, anamnesis, clinicalPicture, diagnosis, labAnalysis, reccommendation, controlCheckupDate, paidSum, reccomendedTherapy);
        }

        public virtual Pet Pet { get; set; }

        private readonly List<CheckupItem> _checkupItems = new List<CheckupItem>();
        public IReadOnlyCollection<CheckupItem> CheckupItems => _checkupItems.AsReadOnly();

        public void AddCheckupItem(int medicamentId, string uom, string medicamentApplication, double price, ICheckupItemInventoryService inventoryService, double amount = 1)
        {
            if (inventoryService.IsItemOutOfStock(medicamentId))
            {
                throw new CheckupItemOutOfStockException($"The item with medicamentId: {medicamentId} is not in stock");
            }

            if (!CheckupItems.Any(i => i.MedicamentId == medicamentId))
            {
                var checkupItem = new CheckupItem(medicamentId, amount, uom, medicamentApplication, price);
                _checkupItems.Add(checkupItem);
            }
            else
            {
                var existing = CheckupItems.FirstOrDefault(i => i.MedicamentId == medicamentId);
                existing.IncreaseAmount(amount);
            }

            if (inventoryService.WillItemBeOutOfStock(medicamentId, amount))
            {
                DomainEvents.Add(new CheckupItemOutOfStock(new CheckupItem(medicamentId, amount, uom, medicamentApplication, price)));
            }
        }

        public void UpdateNextVaccination(DateTime date)
        {
            NextVaccinationDate = date;
        }

        private static void CheckDateValidity(DateTime dateOfVaccination)
        {
            if (dateOfVaccination.Date < DateTime.Today.Date)
                throw new IncorrectCheckupDateException($"The date provided {dateOfVaccination.Date} is smaller than today {DateTime.Today.Date}");
        }
    }
}