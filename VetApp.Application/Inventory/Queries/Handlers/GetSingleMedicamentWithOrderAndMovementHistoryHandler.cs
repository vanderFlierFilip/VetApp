using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Data;
using VDMJasminka.Data.Queries;
using VDMJasminka.Shared.ViewModels;
using static Slapper.AutoMapper;

namespace VDMJasminka.Application.Inventory.Queries.Handlers
{
    public class GetSingleMedicamentWithOrderAndMovementHistoryHandler : IRequestHandler<GetSingleMedicamentWithOrderAndMovementHistory, MedicamentWithOrderAndMovementHistoryViewModel>
    {
        private readonly DapperContext _dapperContext;

        public GetSingleMedicamentWithOrderAndMovementHistoryHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<MedicamentWithOrderAndMovementHistoryViewModel> Handle(GetSingleMedicamentWithOrderAndMovementHistory request, CancellationToken cancellationToken)
        {
            using var connection = _dapperContext.CreateConnection();
            var queryArgs = new { medicament_id = request.MedicamentId };
            connection.Open();
            var vm = new MedicamentWithOrderAndMovementHistoryViewModel();
            vm.Medicament = await connection.QueryFirstAsync<MedicamentInventoryViewModel>(MedicamentInventoryQueries.GetSingleItem, queryArgs);
            vm.Withdrawals = await connection.QueryAsync<MedicamentInventoryWithrawalDto>(MedicamentInventoryQueries.GetInventoryWithdrawals, queryArgs);
            vm.Adjustments = await connection.QueryAsync(MedicamentInventoryQueries.GetInventoryAdjustments, queryArgs);
            vm.Orders = await connection.QueryAsync<MedicamentHistoryOrderDto>(MedicamentInventoryQueries.GetInventoryOrderEntries, queryArgs);
            return vm;
        }
    }
}