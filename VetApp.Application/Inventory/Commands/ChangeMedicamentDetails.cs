using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Inventory.Commands
{
    public class ChangeMedicamentDetails : IRequest
    {
        public ChangeMedicamentDetails(int medicamentId, string name, string uom, bool isMaterial)
        {
            MedicamentId = medicamentId;
            Name = name;
            Uom = uom;
            IsMaterial = isMaterial;
        }

        public int MedicamentId { get; }
        public string Name { get; }
        public string Uom { get; }
        public bool IsMaterial { get; }
    }
}