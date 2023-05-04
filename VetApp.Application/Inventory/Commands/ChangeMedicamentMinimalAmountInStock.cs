using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Inventory.Commands
{
    public class ChangeMedicamentMinimalAmountInStock : IRequest
    {
        public ChangeMedicamentMinimalAmountInStock(int medicamentId, int amount)
        {
            MedicamentId = medicamentId;
            Amount = amount;
        }

        public int MedicamentId { get; }
        public int Amount { get; }
    }
}