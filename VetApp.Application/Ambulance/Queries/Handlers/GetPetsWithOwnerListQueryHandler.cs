using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Dtos.Ambulance;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Data;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetPetsWithOwnerListQueryHandler : IRequestHandler<GetPetsWithOwnerListQuery, IEnumerable<PetSearchViewModel>>
    {
        private readonly VDMJasminkaDbContext _context;
        private readonly IRepository<Owner> _repository;

        public GetPetsWithOwnerListQueryHandler(VDMJasminkaDbContext context, IRepository<Owner> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<IEnumerable<PetSearchViewModel>> Handle(GetPetsWithOwnerListQuery request, CancellationToken cancellationToken)
        {
            // TODO: Add caching for call
            var owners = await _repository.GetAll();

            return owners.ToList().SelectMany(o =>
            {
                return o.Pets.Select(p =>
                {
                    return new PetSearchViewModel
                    {
                        OwnerId = o.Id,
                        PetId = p.Id,
                        PetName = p.PetInformation.Name,
                        OwnerName = o.OwnerInformation.FullName
                    };
                });
            });
        }

    }
}
