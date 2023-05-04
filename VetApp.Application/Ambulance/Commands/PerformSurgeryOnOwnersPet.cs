using MediatR;
using System;
using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Commands
{
    public class PerformSurgeryOnOwnersPet : IRequest
    {
        public PerformSurgeryOnOwnersPet(int ownerId,
                                         int petId,
                                         DateTime dateOfSurgery,
                                         string anamnesis,
                                         string clinicalPicture,
                                         string diagnosis,
                                         string surgicalIntervention,
                                         string labAnalysis,
                                         string reccommendation,
                                         DateTime? controlCheckupDate,
                                         List<CreateCheckupCheckupItemsDto> checkupItems,
                                         decimal? checkupPrice,
                                         string recommendedTherapy)
        {
            OwnerId = ownerId;
            PetId = petId;
            DateOfSurgery = dateOfSurgery;
            Anamnesis = anamnesis;
            ClinicalPicture = clinicalPicture;
            Diagnosis = diagnosis;
            SurgicalIntervention = surgicalIntervention;
            LabAnalysis = labAnalysis;
            Reccommendation = reccommendation;
            ControlCheckupDate = controlCheckupDate;
            CheckupItems = checkupItems;
            CheckupPrice = checkupPrice;
            RecommendedTherapy = recommendedTherapy;
        }

        public int OwnerId { get; }
        public int PetId { get; }
        public DateTime DateOfSurgery { get; }
        public string Anamnesis { get; }
        public string ClinicalPicture { get; }
        public string Diagnosis { get; }
        public string SurgicalIntervention { get; }
        public string LabAnalysis { get; }
        public string Reccommendation { get; }
        public DateTime? ControlCheckupDate { get; }
        public List<CreateCheckupCheckupItemsDto> CheckupItems { get; }
        public decimal? CheckupPrice { get; }
        public string RecommendedTherapy { get; }
    }
}