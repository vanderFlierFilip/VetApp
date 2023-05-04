using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Application.Ambulance.Commands.Handlers
{
    public class RegisterPetToOwnerHandler : IRequestHandler<RegisterPetToOwner>
    {
        private readonly IRepository<Owner> _ownerRepository;

        public RegisterPetToOwnerHandler(IRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Unit> Handle(RegisterPetToOwner command, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.Get(command.OwnerId);

            owner.RegisterNewPet(
                         command.Name,
                         command.AnimalTypeId,
                         command.BreedId,
                         command.DateOfBirth,
                         command.Sex,
                         command.Description,
                         command.Alergy,
                         command.ChipNumber);

            await _ownerRepository.Update(owner);

            return await Unit.Task;
        }

    }
}
