using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Data;
using VDMJasminka.Data.Queries;

namespace VDMJasminka.Application.Inventory.Queries.Handlers
{
    internal class GetSupplierDetailsQueryHandler : IRequestHandler<GetSupplierDetailsQuery, SupplierDetailsViewModel>
    {
        private readonly DapperContext _dapperContext;

        public GetSupplierDetailsQueryHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<SupplierDetailsViewModel> Handle(GetSupplierDetailsQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = MedicamentInventoryQueries.GetMedicamentsSuppliedBySupplier + MedicamentInventoryQueries.GetBalnceForSupplier + MedicamentInventoryQueries.GetSupplierRecievedOrders;
            var vm = new SupplierDetailsViewModel();

            using (var multi = await connection.QueryMultipleAsync(query, new { supplierId = request.Id }))
            {
                var products = await multi.ReadAsync<Product>();
                var balance = await multi.ReadSingleAsync<double>();
                var orders = await multi.ReadAsync<RecievedOrder>();
                vm.Products = products.ToList();
                vm.Balance = balance;
                vm.RecievedOrders = orders.ToList();
            }

            return vm;
        }
    }
}