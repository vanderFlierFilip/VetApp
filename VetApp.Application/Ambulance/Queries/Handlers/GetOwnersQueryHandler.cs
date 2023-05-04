using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Data.Extensions;
using VDMJasminka.Shared.Dto;
using VDMJasminka.Shared.Queries;
using VDMJasminka.Shared.Requests;
using VDMJasminka.Shared.ViewModels;
using System.Linq.Dynamic.Core;
using VDMJasminka.Data;
using Dapper;
using System.ComponentModel.Design;
using System.Data;
using VDMJasminka.Data.Queries;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetOwnersQueryHandler : IRequestHandler<GetOwnersQuery, PagedList<OwnerListViewModel>>
    {
        private readonly DapperContext _dapperContext;

        public GetOwnersQueryHandler(DapperContext depperContext)
        {
            _dapperContext = depperContext;
        }

        public async Task<PagedList<OwnerListViewModel>> Handle(GetOwnersQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dapperContext.CreateConnection();

            var skip = (request.RequestParameters.PageNumber - 1) * request.RequestParameters.PageSize;
            var searchTerm = !string.IsNullOrEmpty(request.RequestParameters.SearchTerm) ?
                request.RequestParameters.SearchTerm.Trim().ToLower() : string.Empty;
            var orderBy = OrderQueryBuilder
                .CreateOrderQuery<OwnerListViewModel>(request.RequestParameters.OrderBy);

            var query = AmbulanceQueries.GetOwnersWithPetsQuery(orderBy);

            var param = new DynamicParameters();
            param.Add("skip", skip, DbType.Int32);
            param.Add("take", request.RequestParameters.PageSize, DbType.Int32);
            param.Add("searchTerm", searchTerm, DbType.String);

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

            var offset = (request.RequestParameters.PageNumber - 1) * (request.RequestParameters.PageSize);

            var orderByQuery = OrderQueryBuilder.CreateOrderQuery<OwnerListViewModel>(request.RequestParameters.OrderBy);

            var items = owners.Distinct().ToList();

            return new PagedList<OwnerListViewModel>(items, items.Count(), request.RequestParameters.PageNumber, request.RequestParameters.PageSize);
        }
    }
}