using MediatR;
using System.Threading.Tasks;
using System.Threading;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;

namespace VDMJasminka.Application.Inventory.Commands.Handlers
{
    internal sealed class ChangeMedicamentDetailsHandler : IRequestHandler<ChangeMedicamentDetails>
    {
        private readonly IRepository<Medicament> _medicamentRepository;

        public ChangeMedicamentDetailsHandler(IRepository<Medicament> medicamentRepository)
        {
            _medicamentRepository = medicamentRepository;
        }

        public async Task<Unit> Handle(ChangeMedicamentDetails request, CancellationToken cancellationToken)
        {
            var medicament = await _medicamentRepository.Get(request.MedicamentId);

            medicament.ChangeDetails(request.Name, request.Uom, request.IsMaterial);

            await _medicamentRepository.Update(medicament);

            return Unit.Value;
        }
    }
}