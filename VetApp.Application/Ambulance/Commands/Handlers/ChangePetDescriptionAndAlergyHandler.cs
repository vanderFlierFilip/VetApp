using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Ambulance.ValueObjects;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Application.Ambulance.Commands.Handlers
{
    internal sealed class ChangePetDescriptionAndAlergyHandler : IRequestHandler<ChangePetDescriptionAndAlergy>
    {
        private readonly IRepository<Owner> _ownerRepository;

        public ChangePetDescriptionAndAlergyHandler(IRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Unit> Handle(ChangePetDescriptionAndAlergy request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.Get(request.OwnerId);

            owner.ChangePetDescriptionAndAlergies(request.PetId, request.Alergy, request.Description);

            await _ownerRepository.Update(owner);

            return await Unit.Task;
        }
    }
}