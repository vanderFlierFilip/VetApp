using Dapper;
using MediatR;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Data;
using VDMJasminka.Data.Queries;
using VDMJasminka.Shared.Dto.Inventory;
using VDMJasminka.Shared.ViewModels;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    internal class GetCreateCheckupViewModelHandler : IRequestHandler<GetCreateCheckupViewModel, CreateCheckupViewModel>
    {
        private readonly DapperContext _dapperContext;

        public GetCreateCheckupViewModelHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<CreateCheckupViewModel> Handle(GetCreateCheckupViewModel request, CancellationToken cancellationToken)
        {
            var sb = new StringBuilder(MedicamentInventoryQueries.GetMedicamentsInStock);
            sb.Append(AmbulanceQueries.GetDiagnosesQuery);
            sb.Append(AmbulanceQueries.GetOwnerByIdQuery);
            sb.Append(AmbulanceQueries.GetPetByOwnerAndPetIdQuery);

            var query = sb.ToString();

            var param = new DynamicParameters();
            param.Add("ownerId", request.OwnerId);
            param.Add("petId", request.PetId);

            using var connection = _dapperContext.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, param);

            var meds = await multi.ReadAsync<MedicamentCheckupDto>();
            var diagnoses = await multi.ReadAsync<string>();
            var owner = await multi.ReadFirstOrDefaultAsync<OwnerViewModel>();
            var pet = await multi.ReadFirstOrDefaultAsync<PetViewModel>();

            return new CreateCheckupViewModel()
            {
                Medicaments = meds.ToList(),
                Diagnoses = diagnoses.ToList(),
                Owner = owner,
                Pet = pet,
            };
        }
    }
}