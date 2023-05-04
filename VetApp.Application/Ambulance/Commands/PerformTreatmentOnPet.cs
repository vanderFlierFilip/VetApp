using MediatR;
using System;
using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Commands
{
    public class PerformTreatmentOnPet : IRequest
    {
        public PerformTreatmentOnPet(int ownerId,
                                     int petId,
                                     DateTime checkupDate,
                                     string anamnesis,
                                     string clinicalPicture,
                                     string diagnosis,
                                     string labAnalysis,
                                     string reccommendation,
                                     DateTime? controlCheckupDate,
                                     List<CreateCheckupCheckupItemsDto> checkupItems,
                                     decimal? paidSum,
                                     string reccommendedTherapy)
        {
            OwnerId = ownerId;
            PetId = petId;
            CheckupDate = checkupDate;
            Anamnesis = anamnesis;
            ClinicalPicture = clinicalPicture;
            Diagnosis = diagnosis;
            LabAnalysis = labAnalysis;
            Reccommendation = reccommendation;
            ControlCheckupDate = controlCheckupDate;
            CheckupItems = checkupItems;
            PaidSum = paidSum;
            ReccommendedTherapy = reccommendedTherapy;
        }

        public int OwnerId { get; }
        public int PetId { get; }
        public DateTime CheckupDate { get; }
        public string Anamnesis { get; }
        public string ClinicalPicture { get; }
        public string Diagnosis { get; }
        public string LabAnalysis { get; }
        public string Reccommendation { get; }
        public DateTime? ControlCheckupDate { get; }
        public List<CreateCheckupCheckupItemsDto> CheckupItems { get; }
        public decimal? PaidSum { get; }
        public string ReccommendedTherapy { get; }
    }
}
