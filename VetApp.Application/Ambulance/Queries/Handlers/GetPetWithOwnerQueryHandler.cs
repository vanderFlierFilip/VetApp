using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Data;
using VDMJasminka.Shared.ViewModels;
using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Data.Queries;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetPetWithOwnerQueryHandler : IRequestHandler<GetPetWithOwnerQuery, PetDetailsViewModel>
    {
        private readonly DapperContext _dapperContext;

        public GetPetWithOwnerQueryHandler(VDMJasminkaDbContext context, IConfiguration configuration, DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<PetDetailsViewModel> Handle(GetPetWithOwnerQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dapperContext.CreateConnection();

            var ownerSql = AmbulanceQueries.GetOwnerByIdQuery;

            var petSql = AmbulanceQueries.GetPetByOwnerAndPetIdQuery;

            var pet = connection.QueryFirstOrDefault<PetViewModel>(petSql, new { ownerId = request.OwnerId, petId = request.PetId });
            var owner = connection.QueryFirstOrDefault<OwnerViewModel>(ownerSql, new { ownerId = request.OwnerId });
            var otherPets = await connection.QueryAsync<OtherPetsModel>(AmbulanceQueries.GetAllPetsExceptOneByIdQuery, new { petId = request.PetId, ownerId = request.OwnerId });
            owner.Pets = otherPets.ToList();
            return new PetDetailsViewModel()
            {
                Pet = pet,
                Owner = owner
            };
        }
    }
}