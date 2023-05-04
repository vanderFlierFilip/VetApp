using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.SupplierAggregate;
using VDMJasminka.Data;
using VDMJasminka.Data.Extensions;
using VDMJasminka.Data.Queries;
using VDMJasminka.Shared.Dto.Inventory;
using VDMJasminka.Shared.Requests;
using VDMJasminka.Shared.ViewModels;

namespace VDMJasminka.Application.Inventory.Queries.Handlers
{
    public class GetSuppliersHandler : IRequestHandler<GetSuppliers, PagedList<SupplierDto>>
    {
        private readonly DapperContext _dapperContext;

        public GetSuppliersHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<PagedList<SupplierDto>> Handle(GetSuppliers request, CancellationToken cancellationToken)
        {
            using var connection = _dapperContext.CreateConnection();

            var skip = (request.RequestParameters.PageNumber - 1) * request.RequestParameters.PageSize;
            var searchTerm = !string.IsNullOrEmpty(request.RequestParameters.SearchTerm) ?
                request.RequestParameters.SearchTerm.Trim().ToLower() : string.Empty;
            var orderBy = OrderQueryBuilder
                .CreateOrderQuery<SupplierDto>(request.RequestParameters.OrderBy);

            var query = MedicamentInventoryQueries.GetSuppliers(orderBy);

            var param = new DynamicParameters();
            param.Add("skip", skip, DbType.Int32);
            param.Add("take", request.RequestParameters.PageSize, DbType.Int32);
            param.Add("searchTerm", searchTerm, DbType.String);

            var suppliers = await connection.QueryAsync<SupplierDto>(query, param);

            var offset = (request.RequestParameters.PageNumber - 1) * (request.RequestParameters.PageSize);

            var orderByQuery = OrderQueryBuilder.CreateOrderQuery<SupplierDto>(request.RequestParameters.OrderBy);

            var items = suppliers.ToList();

            return new PagedList<SupplierDto>(items, items.Count(), request.RequestParameters.PageNumber, request.RequestParameters.PageSize);
        }
    }
}