using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Data;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetAllDiagnosesQueryHandler : IRequestHandler<GetAllDiagnosesQuery, IEnumerable<string>>
    {
        private readonly IConfiguration _configuration;
        private readonly DapperContext _context;

        public GetAllDiagnosesQueryHandler(IConfiguration configuration, DapperContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public Task<IEnumerable<string>> Handle(GetAllDiagnosesQuery request, CancellationToken cancellationToken)
        {
            var sql = "SELECT (name) FROM diagnoses";
            using var connection = _context.CreateConnection();

            connection.Open();

            return Task.FromResult(connection.Query<string>(sql));
        }
    }
}