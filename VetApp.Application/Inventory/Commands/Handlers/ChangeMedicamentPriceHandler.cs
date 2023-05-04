using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.Services;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Inventory.Commands.Handlers
{
    internal sealed class ChangeMedicamentPriceHandler : IRequestHandler<ChangeMedicamentPrice>
    {
        private readonly IRepository<Medicament> _medicamentRepository;

        public ChangeMedicamentPriceHandler(IRepository<Medicament> medicamentRepository)
        {
            _medicamentRepository = medicamentRepository;
        }

        public async Task<Unit> Handle(ChangeMedicamentPrice request, CancellationToken cancellationToken)
        {
            var medicament = await _medicamentRepository.Get(request.MedicamentId);

            medicament.ChangePrice(request.Amount);

            await _medicamentRepository.Update(medicament);

            return Unit.Value;
        }
    }
}