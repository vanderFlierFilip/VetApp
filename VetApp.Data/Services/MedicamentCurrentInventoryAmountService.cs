using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Data.Queries;

namespace VDMJasminka.Data.Services
{
    public class MedicamentCurrentInventoryAmountService : IMedicamentCurrentInventoryAmountService
    {
        private readonly DapperContext _dapperContext;

        public MedicamentCurrentInventoryAmountService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<dynamic> GetCurrentAmountInInventoryAsync(int id)
        {
            using var connection = _dapperContext.CreateConnection();

            var query = MedicamentInventoryQueries.GetCurrentAmountOfMedicamentFromInventoryView;
            var queryArgs = new { medicament_id = id };
            var result = await connection.QueryFirstAsync<dynamic>(query, queryArgs);
            return result;
        }
    }
}