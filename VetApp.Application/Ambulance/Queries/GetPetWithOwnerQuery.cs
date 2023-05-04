using MediatR;
using VDMJasminka.Shared.ViewModels;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetPetWithOwnerQuery : IRequest<PetDetailsViewModel>
    {
        public GetPetWithOwnerQuery(int ownerId, int petId)
        {
            OwnerId = ownerId;
            PetId = petId;
        }

        public int OwnerId { get; }
        public int PetId { get; }
    }
}
