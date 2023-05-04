using Dapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Data;
using VDMJasminka.Data.Extensions;
using VDMJasminka.Shared.Requests;
using VDMJasminka.Shared.ViewModels;
using System.Linq.Dynamic.Core;

namespace VDMJasminka.Application.Inventory.Queries.Handlers
{
    public class GetInventoryStatusHandler : IRequestHandler<GetInventoryStatusQuery, PagedList<MedicamentInventoryViewModel>>
    {
        private readonly DapperContext _dapperContext;

        public GetInventoryStatusHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<PagedList<MedicamentInventoryViewModel>> Handle(GetInventoryStatusQuery request, CancellationToken cancellationToken)
        {
            var searchTerm = !string.IsNullOrEmpty(request.InventoryParameters.SearchTerm) ?
              request.InventoryParameters.SearchTerm.Trim().ToLower() : string.Empty;
            using var connection = _dapperContext.CreateConnection();
            var orderBy = OrderQueryBuilder.CreateOrderQuery<MedicamentInventoryViewModel>(request.InventoryParameters.OrderBy);

            var order = orderBy == "name asc" ? "medicament_name" : orderBy;
            var sql = @$"SELECT * FROM medicament_inventory where strpos(lower(medicament_name), @searchTerm) > 0 order by {order}";
            connection.Open();

            var items = await connection.QueryAsync<MedicamentInventoryViewModel>(sql, new { searchTerm });
            var totalCount = items.Count();

            var offset = (request.InventoryParameters.PageNumber - 1) * (request.InventoryParameters.PageSize);

            if (request.InventoryParameters.InStock != null)
                items = items.Where(x => (x.CurrentAmount > 0) == request.InventoryParameters.InStock);

            //if (request.InventoryParameters.OnOrBelowMinimalAmount != null)
            //    items = items.Where(x => (x.CurrentAmount <= x.MinimalAmount && x.CurrentAmount > 0) == request.InventoryParameters.OnOrBelowMinimalAmount);

            if (request.InventoryParameters.IsMaterial != null)
                items = items.Where(x => x.Material == request.InventoryParameters.IsMaterial);

            items = items.Skip(offset)
                          .Take(request.InventoryParameters.PageSize);

            return new PagedList<MedicamentInventoryViewModel>(items.ToList(), totalCount, request.InventoryParameters.PageNumber, request.InventoryParameters.PageSize);
        }
    }
}