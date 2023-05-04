using MediatR;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetSinglePetFromOwnerQuery : IRequest<CreatePetDto>
    {
        public GetSinglePetFromOwnerQuery(int ownerId, int petId)
        {
            OwnerId = ownerId;
            PetId = petId;
        }

        public int OwnerId { get; }
        public int PetId { get; }
    }
}