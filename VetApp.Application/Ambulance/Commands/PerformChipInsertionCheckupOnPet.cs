using MediatR;
using System;
using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Commands
{
    public class PerformChipInsertionCheckupOnPet : IRequest
    {
        public PerformChipInsertionCheckupOnPet(int ownerId, int petId, DateTime dateOfChipping, string chipNumber, decimal paidSum, List<CreateCheckupCheckupItemsDto> checkupItems)
        {
            OwnerId = ownerId;
            PetId = petId;
            DateOfChipping = dateOfChipping;
            ChipNumber = chipNumber;
            PaidSum = paidSum;
            CheckupItems = checkupItems;
        }

        public int OwnerId { get; }
        public int PetId { get; }
        public DateTime DateOfChipping { get; }
        public string ChipNumber { get; }
        public decimal PaidSum { get; }
        public List<CreateCheckupCheckupItemsDto> CheckupItems { get; }
    }
}
