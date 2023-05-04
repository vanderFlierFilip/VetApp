using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Data;
using VDMJasminka.Data.Queries;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    internal class GetCheckupTypeMedicalRecordHandler : IRequestHandler<GetCheckupTypeMedicalRecord, List<CheckupDto>>
    {
        private readonly DapperContext _dapperContext;

        public GetCheckupTypeMedicalRecordHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<List<CheckupDto>> Handle(GetCheckupTypeMedicalRecord request, CancellationToken cancellationToken)
        {
            var category = request.Pararmeters.CheckupType;
            var checkupsSql = AmbulanceQueries.GetAllCheckupsForPetBasedOnType;
            var connection = _dapperContext.CreateConnection();

            var checkupsDict = new Dictionary<int, CheckupDto>();

            var checkups = await connection.QueryAsync<CheckupDto, CheckupDetailsDto, CheckupDto>(
                checkupsSql, (checkup, details) =>
                {
                    if (!checkupsDict.TryGetValue((int)checkup.Id, out var currentCheckup))
                    {
                        currentCheckup = checkup;
                        checkupsDict.Add((int)currentCheckup.Id, currentCheckup);
                    }

                    currentCheckup.CheckupDetails.Add(details);
                    return currentCheckup;
                },
                new { petId = request.PetId, checkupType = request.Pararmeters.CheckupType }
                , splitOn: "split"
            );

            return checkups.Distinct().ToList();
        }
    }
}