using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Data;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetLatestCheckupForOwnerAndPetQueryHandler : IRequestHandler<GetLatestCheckupForOwnerAndPetQuery, CheckupDto>
    {
        private readonly VDMJasminkaDbContext _dbContext;

        public GetLatestCheckupForOwnerAndPetQueryHandler(VDMJasminkaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CheckupDto> Handle(GetLatestCheckupForOwnerAndPetQuery request, CancellationToken cancellationToken)
        {
            var res = _dbContext.Checkups.Where(checkup => checkup.PetId == request.PetId).ToList().OrderByDescending(x => x.Id).First();
            return res.ToDto();
        }
    }
}
