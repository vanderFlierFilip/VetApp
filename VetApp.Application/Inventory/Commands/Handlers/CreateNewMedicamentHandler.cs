using MediatR;
using System.Threading.Tasks;
using System.Threading;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;

namespace VDMJasminka.Application.Inventory.Commands.Handlers
{
    internal sealed class CreateNewMedicamentHandler : IRequestHandler<CreateNewMedicament, int>
    {
        private readonly IRepository<Medicament> _medicamentRepository;

        public CreateNewMedicamentHandler(IRepository<Medicament> medicamentRepository)
        {
            _medicamentRepository = medicamentRepository;
        }

        public async Task<int> Handle(CreateNewMedicament request, CancellationToken cancellationToken)
        {
            var medicament = new Medicament(request.Name, request.Uom, request.Price, request.MinimalAmount, request.IsMaterial);
            var newMed = await _medicamentRepository.Add(medicament);
            return newMed.Id;
        }
    }
}