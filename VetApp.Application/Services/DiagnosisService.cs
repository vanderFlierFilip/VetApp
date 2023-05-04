using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Common;

namespace VDMJasminka.Application.Services
{
    public class DiagnosisService : IDiagnosisService
    {
        private readonly IConfiguration _configuration;

        public DiagnosisService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task CheckIfDiagnosisExistsOrPersistAsync(string diagnosis)
        {
            var connectionString = _configuration.GetConnectionString("DVMJasminka");
            var sql = "SELECT (name) FROM diagnoses";
            
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
           
            var result = await connection.QueryAsync<string>(sql);
            var compare = result.FirstOrDefault(r => r == diagnosis);
            
            if (compare == null && !string.IsNullOrEmpty(diagnosis))
            {
                connection.Execute(@"INSERT INTO diagnoses (name) VALUES (@diagnosis)", new { diagnosis });
            }
        }
    }
}
