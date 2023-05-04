using MediatR;
using System;
using System.Collections.Generic;
using VDMJasminka.Shared.ViewModels;

namespace VDMJasminka.Application.Inventory.Queries
{
    public class GetSingleMedicamentWithOrderAndMovementHistory : IRequest<MedicamentWithOrderAndMovementHistoryViewModel>
    {
        public GetSingleMedicamentWithOrderAndMovementHistory(int medicamentId)
        {
            MedicamentId = medicamentId;
        }

        public int MedicamentId { get; }
    }

   

}
