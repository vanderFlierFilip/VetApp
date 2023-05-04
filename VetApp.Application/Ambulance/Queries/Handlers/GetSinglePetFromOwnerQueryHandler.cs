using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetSinglePetFromOwnerQueryHandler : IRequestHandler<GetSinglePetFromOwnerQuery, CreatePetDto>
    {
        private readonly IRepository<Owner> _ownerRepository;

        public GetSinglePetFromOwnerQueryHandler(IRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<CreatePetDto> Handle(GetSinglePetFromOwnerQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.Get(request.OwnerId);
            var pet = owner.GetPet(request.PetId);

            return pet.ToDto();
        }
    }
}