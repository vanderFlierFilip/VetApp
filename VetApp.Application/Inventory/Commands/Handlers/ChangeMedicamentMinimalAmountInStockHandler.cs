using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.Services;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Inventory.Commands.Handlers
{
    internal sealed class ChangeMedicamentMinimalAmountInStockHandler : IRequestHandler<ChangeMedicamentMinimalAmountInStock>
    {
        private readonly IRepository<Medicament> _medicamentRepository;

        public ChangeMedicamentMinimalAmountInStockHandler(IRepository<Medicament> medicamentRepository)
        {
            _medicamentRepository = medicamentRepository;
        }

        public async Task<Unit> Handle(ChangeMedicamentMinimalAmountInStock request, CancellationToken cancellationToken)
        {
            var medicament = await _medicamentRepository.Get(request.MedicamentId);

            medicament.ChangeMinimalAmountInStock(request.Amount);

            await _medicamentRepository.Update(medicament);

            return Unit.Value;
        }
    }
}