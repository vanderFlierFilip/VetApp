using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Inventory.Commands
{
    public class ChangeMedicamentPrice : IRequest
    {
        public ChangeMedicamentPrice(int medicamentId, double amount)
        {
            MedicamentId = medicamentId;
            Amount = amount;
        }

        public int MedicamentId { get; }
        public double Amount { get; }
    }
}