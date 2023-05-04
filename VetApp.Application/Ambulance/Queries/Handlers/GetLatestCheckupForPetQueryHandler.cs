using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetLatestCheckupForPetQueryHandler : IRequestHandler<GetLatestCheckupForPet, int>
    {
        private readonly IRepository<Owner> _ownerRepository;

        public GetLatestCheckupForPetQueryHandler(IRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<int> Handle(GetLatestCheckupForPet request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.Get(request.OwnerId);
            var pet = owner.GetPet(request.PetId);
            var checkupId = pet.Checkups
                .FirstOrDefault(checkup => checkup.Date.Value.Date == pet.LastVisit.Value.Date
                && checkup.CheckupType == request.Category).Id;
            return checkupId;
        }
    }
}
