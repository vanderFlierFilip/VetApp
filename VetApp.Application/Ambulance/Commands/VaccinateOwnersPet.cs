using MediatR;
using System;
using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Commands
{
    public class VaccinateOwnersPet : IRequest
    {
        public VaccinateOwnersPet(int ownerId, int petId, string typeOfVaccine, decimal priceOfCheckup, List<CreateCheckupCheckupItemsDto> checkupItems, DateTime? nextVaccinationDate)
        {
            OwnerId = ownerId;
            PetId = petId;
            TypeOfVaccine = typeOfVaccine;
            PriceOfCheckup = priceOfCheckup;
            CheckupItems = checkupItems;
            NextVaccinationDate = nextVaccinationDate;
        }
        public int OwnerId { get; }
        public int PetId { get; }
        public DateTime DateOfVaccination { get; }
        public string TypeOfVaccine { get; }
        public decimal PriceOfCheckup { get; }
        public List<CreateCheckupCheckupItemsDto> CheckupItems { get; }
        public DateTime? NextVaccinationDate { get; }
    }
}
