using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Data;
using VDMJasminka.Data.Queries;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Shared.ViewModels;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetSingleOwnerByIdQueryHandler : IRequestHandler<GetSingleOwnerByIdQuery, OwnerListViewModel>
    {
        private readonly DapperContext _dapperContext;

        public GetSingleOwnerByIdQueryHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<OwnerListViewModel> Handle(GetSingleOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = AmbulanceQueries.GetOwnerWithPetsQuery;

            var param = new DynamicParameters();
            param.Add("ownerId", request.OwnerId, DbType.Int32);

            var ownersDict = new Dictionary<int, OwnerListViewModel>();

            var owners = await connection.QueryAsync<OwnerListViewModel, OwnerListPetViewModel, OwnerListViewModel>(query,
                (owner, pet) =>
                {
                    if (!ownersDict.TryGetValue(owner.Id, out var currentOwner))
                    {
                        currentOwner = owner;
                        ownersDict.Add(owner.Id, owner);
                    }
                    currentOwner.Pets.Add(pet);
                    return currentOwner;
                }, param, splitOn: "pet_id");

            return owners.FirstOrDefault();
        }
    }
}