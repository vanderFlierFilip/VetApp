using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Exceptions;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Application.Ambulance.Commands.Handlers
{
    public class UpdateOwnerLivingAddressHandler : IRequestHandler<UpdateOwnerLivingAddress>
    {
        private readonly IRepository<Owner> _ownerRepository;

        public UpdateOwnerLivingAddressHandler(IRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Unit> Handle(UpdateOwnerLivingAddress request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.Get(request.OwnerId);

            if (owner == null)
            {
                throw new OwnerNotFoundException();
            }

            owner.ChangeLivingAddress(request.Address, request.Location, request.Municipality);

            await _ownerRepository.Update(owner);

            return await Unit.Task;
        }
    }
}