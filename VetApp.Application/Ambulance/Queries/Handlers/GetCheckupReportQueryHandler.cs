using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Data;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetCheckupReportQueryHandler : IRequestHandler<GetCheckupReportQuery, CheckupReportViewModel>
    {
        private readonly IRepository<Owner> _ownerRepository;
        private readonly VDMJasminkaDbContext _context;

        public GetCheckupReportQueryHandler(IRepository<Owner> ownerRepository, VDMJasminkaDbContext context)
        {
            _ownerRepository = ownerRepository;
            _context = context;
        }

        public async Task<CheckupReportViewModel> Handle(GetCheckupReportQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.Get(request.OwnerId);
            var pet = owner.GetPet(request.PetId);
            var checkup = _context.Checkups
                .Include(x => x.CheckupItems)
                .ThenInclude(x => x.Medicament)
                .FirstOrDefault(checkup => checkup.Id == request.CheckupId);

            return new CheckupReportViewModel()
            {
                Owner = owner.ToDto(),
                Pet = pet.ToDto(),
                Checkup = checkup.ToDto()
            };
        }
    }
}
