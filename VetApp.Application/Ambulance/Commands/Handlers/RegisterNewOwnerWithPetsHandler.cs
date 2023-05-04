using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Dtos.Ambulance;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Commands.Handlers
{
    internal sealed class RegisterNewOwnerWithPetsHandler : IRequestHandler<RegisterNewOwnerWithPets, ReadOwnerDto>
    {
        private readonly IRepository<Owner> _ownerRepository;

        public RegisterNewOwnerWithPetsHandler(IRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        async Task<ReadOwnerDto> IRequestHandler<RegisterNewOwnerWithPets, ReadOwnerDto>.Handle(RegisterNewOwnerWithPets command, CancellationToken cancellationToken)
        {
            var newOwner = new Owner(command.FullName,
                                     command.Address,
                                     command.Location,
                                     command.PhoneNumber,
                                     command.Municipality,
                                     command.SSN,
                                     null, // TODO: remove later from ctor (Owner.State) -> not needed anymore
                                     command.AdditionalInfo,
                                     command.Email);

            foreach (var pet in command.Pets)
            {
                newOwner.RegisterNewPet(pet.Name, pet.SpeciesId, pet.BreedId, pet.DateOfBirth, pet.Sex, pet.Description, pet.Alergy, pet.ChipNumber);
            }

            var created = await _ownerRepository.Add(newOwner);

            return created.ToReadDto();
        }
    }
}