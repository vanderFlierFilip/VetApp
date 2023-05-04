using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.Services;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetSingleMedicamentByIdQueryHandler : IRequestHandler<GetSingleMedicamentByIdQuery, MedicamentDto>
    {
        private readonly IRepository<Medicament> _medicamentRepository;

        public GetSingleMedicamentByIdQueryHandler(IRepository<Medicament> medicamentRepository)
        {
            _medicamentRepository = medicamentRepository;
        }

        public async Task<MedicamentDto> Handle(GetSingleMedicamentByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _medicamentRepository.Get(request.MedicamentId);
            return model.ToDto();
        }
    }
}