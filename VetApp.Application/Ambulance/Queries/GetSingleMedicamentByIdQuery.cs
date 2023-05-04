using MediatR;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetSingleMedicamentByIdQuery : IRequest<MedicamentDto>
    {
        public int MedicamentId { get; }
        public GetSingleMedicamentByIdQuery(int medicamentId)
        {
            MedicamentId = medicamentId;
        }
    }
}
